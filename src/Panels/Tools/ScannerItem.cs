using Pokebot.Factories.Versions;
using System;

namespace Pokebot.Panels.Tools
{
    public class ScannerItem
    {
        public string Name { get; }
        public VersionCode[] SupportedVersions { get; }
        public Func<ScannerPanel> Create { get; }

        public ScannerItem(string name, VersionCode[] versions, Func<ScannerPanel> create)
        {
            Name = name;
            SupportedVersions = versions;
            Create = create;
        }

        public override string ToString() => Name;
    }
}
