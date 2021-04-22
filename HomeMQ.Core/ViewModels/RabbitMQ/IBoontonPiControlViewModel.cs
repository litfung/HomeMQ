using HomeMQ.Models;
using System.Threading.Tasks;

namespace HomeMQ.Core.ViewModels
{
    public interface IBoontonPiControlViewModel : IRabbitControlViewModel<IBoontonPi>
    {
        Task PollUpdates();
    }
}