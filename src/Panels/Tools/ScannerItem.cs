using System;

namespace Pokebot.Panels.Tools
{
    public class ScannerItem
    {
        public string Name { get; }
        public int[] SupportedGenerations { get; }
        public Func<ScannerPanel> Create { get; }

        public ScannerItem(string name, int[] generations, Func<ScannerPanel> create)
        {
            Name = name;
            SupportedGenerations = generations;
            Create = create;
        }

        public override string ToString() => Name;
    }
}
