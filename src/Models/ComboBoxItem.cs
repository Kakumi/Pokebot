using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
