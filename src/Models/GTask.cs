namespace Pokebot.Models
{
    public class GTask
    {
        public string Name { get; }
        public bool IsActive { get; }
        public int Previous { get; }
        public int Next { get; }
        public int Priority { get; }
        public byte[] Data { get; }

        public GTask(string name, bool isActive, int previous, int next, int priority, byte[] data)
        {
            Name = name;
            IsActive = isActive;
            Previous = previous;
            Next = next;
            Priority = priority;
            Data = data;
        }
    }
}
