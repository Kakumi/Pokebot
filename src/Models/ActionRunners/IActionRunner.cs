namespace Pokebot.Models.ActionRunners
{
    public interface IActionRunner
    {
        bool ExecuteStarter(int indexStarter);
        bool Spin();
        bool Escape();
        bool UseRegisteredItem();
    }
}
