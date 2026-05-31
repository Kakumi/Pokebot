using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pokebot.Server
{
    public sealed class PokebotServer : IDisposable
    {
        private readonly object _sync = new object();
        private readonly List<WebSocket> _clients = new List<WebSocket>();

        private HttpListener? _listener;
        private CancellationTokenSource? _cts;
        private Task? _serverTask;
        private bool _disposed;

        public string Url { get; private set; } = string.Empty;
        public string WebSocketUrl { get; private set; } = string.Empty;

        public string Start()
        {
            if (_listener != null)
            {
                return WebSocketUrl;
            }

            var port = 57896;
            Url = $"http://127.0.0.1:{port}/";
            WebSocketUrl = $"ws://127.0.0.1:{port}/ws/";

            _listener = new HttpListener();
            _listener.Prefixes.Add(Url);
            _listener.Start();

            _cts = new CancellationTokenSource();
            _serverTask = Task.Run(() => RunServerAsync(_cts.Token));

            return WebSocketUrl;
        }

        public async Task<int> SendMessageAsync(string message, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(message))
            {
                return 0;
            }

            byte[] payload = Encoding.UTF8.GetBytes(message);
            ArraySegment<byte> buffer = new ArraySegment<byte>(payload);
            WebSocket[] clients = GetClientsSnapshot();
            int sentCount = 0;

            foreach (var client in clients)
            {
                if (client.State != WebSocketState.Open)
                {
                    RemoveClient(client);
                    continue;
                }

                try
                {
                    await client.SendAsync(buffer, WebSocketMessageType.Text, true, cancellationToken);
                    sentCount++;
                }
                catch
                {
                    RemoveClient(client);
                    try
                    {
                        client.Dispose();
                    }
                    catch { }
                }
            }

            return sentCount;
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

                _ = Task.Run(() => HandleRequestAsync(context, cancellationToken), cancellationToken);
            }
        }

        private async Task HandleRequestAsync(HttpListenerContext context, CancellationToken cancellationToken)
        {
            if (!context.Request.IsWebSocketRequest)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.Close();
                return;
            }

            var path = context.Request.Url?.AbsolutePath ?? "/";
            if (!string.Equals(path, "/ws/", StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.Close();
                return;
            }

            HttpListenerWebSocketContext webSocketContext;
            try
            {
                webSocketContext = await context.AcceptWebSocketAsync(null);
            }
            catch
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.Close();
                return;
            }

            var socket = webSocketContext.WebSocket;
            AddClient(socket);

            try
            {
                await ReceiveLoopAsync(socket, cancellationToken);
            }
            finally
            {
                RemoveClient(socket);
                try
                {
                    socket.Dispose();
                }
                catch { }
            }
        }

        private async Task ReceiveLoopAsync(WebSocket socket, CancellationToken cancellationToken)
        {
            byte[] buffer = new byte[1024];

            while (!cancellationToken.IsCancellationRequested && socket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result;

                try
                {
                    result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
                }
                catch
                {
                    break;
                }

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    try
                    {
                        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                    }
                    catch { }

                    break;
                }
            }
        }

        private WebSocket[] GetClientsSnapshot()
        {
            lock (_sync)
            {
                return _clients.ToArray();
            }
        }

        private void AddClient(WebSocket socket)
        {
            lock (_sync)
            {
                _clients.Add(socket);
            }
        }

        private void RemoveClient(WebSocket socket)
        {
            lock (_sync)
            {
                _clients.Remove(socket);
            }
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

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

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

            foreach (var client in GetClientsSnapshot())
            {
                try
                {
                    if (client.State == WebSocketState.Open || client.State == WebSocketState.CloseReceived)
                    {
                        client
                            .CloseAsync(WebSocketCloseStatus.NormalClosure, "Server shutting down", CancellationToken.None)
                            .GetAwaiter()
                            .GetResult();
                    }
                }
                catch { }

                try
                {
                    client.Dispose();
                }
                catch { }
            }

            lock (_sync)
            {
                _clients.Clear();
            }

            _cts?.Dispose();
        }
    }
}
