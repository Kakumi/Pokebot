using System.Linq;
using Pokebot.Factories.Versions;

namespace Pokebot.Panels.Tools
{
    public static class ScannerRegistry
    {
        private static readonly ScannerItem[] _all = new ScannerItem[]
        {
            new ScannerItem(
                "gMain",
                new[] { VersionCode.Emerald, VersionCode.FireRed, VersionCode.LeafGreen },
                () => new Emerald.GMainScannerPanel()
            ),
            new ScannerItem("gMain", new[] { VersionCode.Ruby, VersionCode.Sapphire }, () => new RubySapphire.GMainScannerPanel()),
            new ScannerItem(
                "gObjectEvents",
                new[] { VersionCode.Emerald, VersionCode.FireRed, VersionCode.LeafGreen },
                () => new Emerald.GObjectEventsScannerPanel()
            ),
            new ScannerItem("gObjectEvents", new[] { VersionCode.Ruby, VersionCode.Sapphire }, () => new RubySapphire.GObjectEventsScannerPanel()),
            new ScannerItem(
                "gPlayerAvatar",
                new[] { VersionCode.Emerald, VersionCode.Ruby, VersionCode.Sapphire, VersionCode.FireRed, VersionCode.LeafGreen },
                () => new Emerald.GPlayerAvatarScannerPanel()
            ),
            new ScannerItem(
                "gTasks",
                new[] { VersionCode.Emerald, VersionCode.Ruby, VersionCode.Sapphire, VersionCode.FireRed, VersionCode.LeafGreen },
                () => new Emerald.GTasksScannerPanel()
            ),
            new ScannerItem(
                "gSpeciesInfo",
                new[] { VersionCode.Emerald, VersionCode.Ruby, VersionCode.Sapphire, VersionCode.FireRed, VersionCode.LeafGreen },
                () => new Emerald.GSpeciesInfoScannerPanel()
            ),
            new ScannerItem(
                "gEnemyParty",
                new[] { VersionCode.Emerald, VersionCode.Ruby, VersionCode.Sapphire, VersionCode.FireRed, VersionCode.LeafGreen },
                () => new Emerald.GEnemyPartyScannerPanel()
            ),
            new ScannerItem(
                "gPlayerParty",
                new[] { VersionCode.Emerald, VersionCode.Ruby, VersionCode.Sapphire, VersionCode.FireRed, VersionCode.LeafGreen },
                () => new Emerald.GPlayerPartyScannerPanel()
            ),
            new ScannerItem(
                "gActionSelectionCursor",
                new[] { VersionCode.Emerald, VersionCode.Ruby, VersionCode.Sapphire, VersionCode.FireRed, VersionCode.LeafGreen },
                () => new Emerald.GActionSelectionCursorScannerPanel()
            ),
            new ScannerItem(
                "gRngValue",
                new[] { VersionCode.Emerald, VersionCode.Ruby, VersionCode.Sapphire, VersionCode.FireRed, VersionCode.LeafGreen },
                () => new Emerald.GRngValueScannerPanel()
            ),
            new ScannerItem(
                "gSaveBlock2Ptr",
                new[] { VersionCode.Emerald, VersionCode.Ruby, VersionCode.Sapphire, VersionCode.FireRed, VersionCode.LeafGreen },
                () => new Emerald.GSaveBlock2PtrScannerPanel()
            ),
        };

        public static ScannerItem[] GetForVersion(VersionCode version) => _all.Where(s => s.SupportedVersions.Contains(version)).ToArray();
    }
}
