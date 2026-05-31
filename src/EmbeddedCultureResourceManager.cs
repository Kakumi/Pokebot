using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Pokebot
{
    /// <summary>
    /// A ResourceManager that reads localized resources from streams embedded in the main
    /// assembly instead of satellite DLLs.  Each culture "xx" must have its compiled
    /// .resources stream embedded under the name "&lt;baseName&gt;.xx.resources".
    /// Falls back to the invariant/English resource when a key or culture is missing.
    /// </summary>
    internal sealed class EmbeddedCultureResourceManager : ResourceManager
    {
        private readonly Assembly _assembly;
        private readonly string _baseName;
        private readonly Dictionary<string, ResourceSet?> _cache = new Dictionary<string, ResourceSet?>();

        internal EmbeddedCultureResourceManager(string baseName, Assembly assembly)
            : base(baseName, assembly)
        {
            _baseName = baseName;
            _assembly = assembly;
        }

        public override string GetString(string name, CultureInfo culture)
        {
            if (culture != null && culture.Name.Length > 0 && culture.Name != "en")
            {
                var set = GetEmbeddedSet(culture.Name);
                if (set != null)
                {
                    var value = set.GetString(name);
                    if (value != null)
                    {
                        return value;
                    }
                }
            }

            return base.GetString(name, CultureInfo.InvariantCulture);
        }

        private ResourceSet? GetEmbeddedSet(string cultureName)
        {
            if (_cache.TryGetValue(cultureName, out var cached))
            {
                return cached;
            }

            var streamName = $"{_baseName}.{cultureName}.resources";
            var stream = _assembly.GetManifestResourceStream(streamName);
            if (stream == null)
            {
                _cache[cultureName] = null;
                return null;
            }

            var set = new ResourceSet(stream);
            _cache[cultureName] = set;
            return set;
        }
    }
}
