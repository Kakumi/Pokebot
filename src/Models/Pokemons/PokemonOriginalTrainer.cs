namespace Pokebot.Models.Pokemons
{
    public class PokemonOriginalTrainer
    {
        public int Id { get; }
        public int SecretId { get; }
        public string Name { get; }

        public PokemonOriginalTrainer(int id, int secretId, string name)
        {
            Id = id;
            SecretId = secretId;
            Name = name;
        }
    }
}
