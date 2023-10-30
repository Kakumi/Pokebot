using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.ActionRunners
{
    public interface IActionRunner
    {
        bool ExecuteStarter(int indexStarter);
        bool Spin();
        bool Escape();
    }
}
