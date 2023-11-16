namespace Pokebot.Models
{
    public class ComboBoxItem
    {
        public int Id { get; }
        public string Name { get; }

        public ComboBoxItem(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
