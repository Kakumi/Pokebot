using System.Linq;

namespace Pokebot.Panels.Tools
{
    public static class ScannerRegistry
    {
        private static readonly ScannerItem[] _all = new ScannerItem[]
        {
            new ScannerItem("gMain",         new[] { 3 }, () => new GMainScannerPanel()),
            new ScannerItem("gObjectEvents", new[] { 3 }, () => new GObjectEventsScannerPanel()),
            new ScannerItem("gPlayerAvatar", new[] { 3 }, () => new GPlayerAvatarScannerPanel()),
            new ScannerItem("gTasks",        new[] { 3 }, () => new GTasksScannerPanel()),
            new ScannerItem("gSpeciesInfo",  new[] { 3 }, () => new GSpeciesInfoScannerPanel()),
            new ScannerItem("gEnemyParty",              new[] { 3 }, () => new GEnemyPartyScannerPanel()),
            new ScannerItem("gPlayerParty",             new[] { 3 }, () => new GPlayerPartyScannerPanel()),
            new ScannerItem("gActionSelectionCursor",   new[] { 3 }, () => new GActionSelectionCursorScannerPanel()),
            new ScannerItem("gRngValue",                new[] { 3 }, () => new GRngValueScannerPanel()),
            new ScannerItem("gSaveBlock2Ptr",           new[] { 3 }, () => new GSaveBlock2PtrScannerPanel()),
        };

        public static ScannerItem[] GetForGeneration(int generation)
            => _all.Where(s => s.SupportedGenerations.Contains(generation)).ToArray();
    }
}
