using System.Threading.Tasks;
using TasksService.BO;

namespace TasksService.Streams.Contracts
{
    public interface ITaskBusinessEventsStream
    {
        Task StreamAboutTaskCreated(PopugTask task);
        Task StreamAboutTaskAssigned(PopugTask task);
        Task StreamAboutTaskClosed(PopugTask task);
    }
}