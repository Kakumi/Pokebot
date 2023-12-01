using Newtonsoft.Json;
using Pokebot.Models.Player;

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
