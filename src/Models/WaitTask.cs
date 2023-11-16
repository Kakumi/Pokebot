using System.ComponentModel;
using System.Threading.Tasks;

namespace Pokebot.Models
{
    public class WaitTask
    {
        public double Seconds { get; set; }
        private readonly BackgroundWorker _worker;

        public WaitTask(double seconds)
        {
            Seconds = seconds;
            _worker = new BackgroundWorker();
            _worker.DoWork += (s, e) =>
            {
                Task.Delay((int)(Seconds * 1000)).Wait();
            };
        }

        public bool IsBusy => _worker.IsBusy;

        public void Start()
        {
            if (!IsBusy)
            {
                _worker.RunWorkerAsync();
            }
        }
    }
}
