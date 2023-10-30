using Newtonsoft.Json;
using Pokebot.Models.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Config
{
    public class BotStarterConfig
    {
        public Position Position { get; }
        public PlayerFacingDirection Facing { get; }

        [JsonConstructor]
        public BotStarterConfig(Position position, PlayerFacingDirection facing)
        {
            Position = position;
            Facing = facing;
        }
    }
}
