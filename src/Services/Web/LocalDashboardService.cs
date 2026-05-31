using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Pokebot.Utils;

namespace Pokebot.Services.Web
{
    public sealed class LocalDashboardService : IDisposable
    {
        private readonly Control _invoker;
        private readonly Func<DashboardState> _stateProvider;
        private readonly Dictionary<string, Action> _actions;
        private readonly List<string> _recentLogs = new List<string>();
        private readonly object _sync = new object();
        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        };
        private readonly Assembly _assembly;
        private readonly string _resourcePrefix;
        private readonly Dictionary<string, string> _resourceMap;

        private HttpListener? _listener;
        private CancellationTokenSource? _cts;
        private Task? _serverTask;
        private bool _disposed;

        public string Url { get; private set; } = string.Empty;

        public LocalDashboardService(Control invoker, Func<DashboardState> stateProvider, Dictionary<string, Action> actions)
        {
            _invoker = invoker;
            _stateProvider = stateProvider;
            _actions = actions;
            _assembly = typeof(LocalDashboardService).Assembly;
            _resourcePrefix = $"{_assembly.GetName().Name}.WebAssets.";
            _resourceMap = BuildResourceMap();
            Log.LogReceived += Log_LogReceived;
        }

        public string Start()
        {
            if (_listener != null)
            {
                return Url;
            }

            var port = FindFreePort();
            Url = $"http://127.0.0.1:{port}/";

            _listener = new HttpListener();
            _listener.Prefixes.Add(Url);
            _listener.Start();

            _cts = new CancellationTokenSource();
            _serverTask = Task.Run(() => RunServerAsync(_cts.Token));

            return Url;
        }

        public void OpenInBrowser()
        {
            if (string.IsNullOrWhiteSpace(Url))
            {
                return;
            }

            System.Diagnostics.Process.Start(Url);
        }

        private async Task RunServerAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested && _listener != null)
            {
                HttpListenerContext context;

                try
                {
                    context = await _listener.GetContextAsync();
                }
                catch (Exception ex) when (ex is ObjectDisposedException || ex is HttpListenerException)
                {
                    break;
                }

                _ = Task.Run(() => HandleRequest(context), cancellationToken);
            }
        }

        private void HandleRequest(HttpListenerContext context)
        {
            try
            {
                var path = context.Request.Url?.AbsolutePath ?? "/";

                if (path == "/api/state")
                {
                    var state = InvokeOnUiThread(_stateProvider);
                    state.DashboardUrl = Url;
                    state.RecentLogs = GetRecentLogs();
                    WriteJson(context.Response, state);
                    return;
                }

                if (path.StartsWith("/api/action/", StringComparison.OrdinalIgnoreCase))
                {
                    if (context.Request.HttpMethod != "POST")
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                        context.Response.Close();
                        return;
                    }

                    var actionName = path.Substring("/api/action/".Length);
                    if (_actions.TryGetValue(actionName, out var action))
                    {
                        InvokeOnUiThread(action);
                        WriteJson(context.Response, new { success = true });
                        return;
                    }

                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    WriteJson(context.Response, new { success = false, error = "Unknown action" });
                    return;
                }

                ServeStaticAsset(context.Response, path);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                try
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    WriteJson(context.Response, new { success = false, error = ex.Message });
                }
                catch { }
            }
        }

        private Dictionary<string, string> BuildResourceMap()
        {
            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var resourceName in _assembly.GetManifestResourceNames())
            {
                if (!resourceName.StartsWith(_resourcePrefix, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var relativeName = resourceName.Substring(_resourcePrefix.Length);
                var extension = GetExtension(relativeName);
                var fileName = relativeName;

                if (!string.IsNullOrEmpty(extension))
                {
                    fileName = relativeName.Substring(0, relativeName.Length - extension.Length).Replace('.', '-') + extension;
                }

                map["/" + fileName] = resourceName;
            }

            return map;
        }

        private void ServeStaticAsset(HttpListenerResponse response, string path)
        {
            var normalizedPath = string.IsNullOrWhiteSpace(path) || path == "/" ? "/index.html" : path;

            if (TryWriteEmbeddedFile(response, normalizedPath))
            {
                return;
            }

            if (!normalizedPath.StartsWith("/api/", StringComparison.OrdinalIgnoreCase) && TryWriteEmbeddedFile(response, "/index.html"))
            {
                return;
            }

            response.StatusCode = (int)HttpStatusCode.NotFound;
            WriteJson(response, new { success = false, error = "Not found" });
        }

        private bool TryWriteEmbeddedFile(HttpListenerResponse response, string path)
        {
            if (!_resourceMap.TryGetValue(path, out var resourceName))
            {
                return false;
            }

            using var stream = _assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                return false;
            }

            using var memory = new MemoryStream();
            stream.CopyTo(memory);
            var bytes = memory.ToArray();

            response.ContentType = GetContentType(path);
            response.ContentLength64 = bytes.Length;
            using var output = response.OutputStream;
            output.Write(bytes, 0, bytes.Length);
            return true;
        }

        private void InvokeOnUiThread(Action action)
        {
            if (_invoker.IsDisposed)
            {
                return;
            }

            if (_invoker.InvokeRequired)
            {
                _invoker.BeginInvoke(action);
                return;
            }

            action();
        }

        private T InvokeOnUiThread<T>(Func<T> func)
        {
            if (_invoker.IsDisposed)
            {
                return func();
            }

            if (_invoker.InvokeRequired)
            {
                return (T)_invoker.Invoke(func);
            }

            return func();
        }

        private void Log_LogReceived(LogEventArgs e)
        {
            lock (_sync)
            {
                _recentLogs.Add($"[{e.Level}] {e.Message}");
                if (_recentLogs.Count > 20)
                {
                    _recentLogs.RemoveAt(0);
                }
            }
        }

        private List<string> GetRecentLogs()
        {
            lock (_sync)
            {
                return new List<string>(_recentLogs);
            }
        }

        private void WriteJson(HttpListenerResponse response, object payload)
        {
            var json = JsonConvert.SerializeObject(payload, _jsonSettings);
            var bytes = Encoding.UTF8.GetBytes(json);
            response.ContentType = "application/json; charset=utf-8";
            response.ContentEncoding = Encoding.UTF8;
            response.ContentLength64 = bytes.Length;
            using var output = response.OutputStream;
            output.Write(bytes, 0, bytes.Length);
        }

        private static int FindFreePort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            try
            {
                listener.Start();
                return ((IPEndPoint)listener.LocalEndpoint).Port;
            }
            finally
            {
                listener.Stop();
            }
        }

        private static string GetExtension(string resourceName)
        {
            var knownExtensions = new[] { ".html", ".js", ".css", ".json", ".ico", ".png", ".jpg", ".jpeg", ".svg", ".woff", ".woff2" };

            foreach (var extension in knownExtensions)
            {
                if (resourceName.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
                {
                    return extension;
                }
            }

            return Path.GetExtension(resourceName);
        }

        private static string GetContentType(string path)
        {
            var extension = Path.GetExtension(path).ToLowerInvariant();
            return extension switch
            {
                ".html" => "text/html; charset=utf-8",
                ".js" => "application/javascript; charset=utf-8",
                ".css" => "text/css; charset=utf-8",
                ".json" => "application/json; charset=utf-8",
                ".ico" => "image/x-icon",
                ".png" => "image/png",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".svg" => "image/svg+xml",
                ".woff" => "font/woff",
                ".woff2" => "font/woff2",
                _ => "application/octet-stream",
            };
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            Log.LogReceived -= Log_LogReceived;

            try
            {
                _cts?.Cancel();
            }
            catch { }

            try
            {
                _listener?.Stop();
                _listener?.Close();
            }
            catch { }

            _cts?.Dispose();
        }
    }
}
