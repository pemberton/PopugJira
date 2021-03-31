using System.Threading.Tasks;
using TasksService.BO;
using TasksService.Streams.Contracts;

namespace TasksService.Streams
{
    public class TaskBusinessEventsStream : ITaskBusinessEventsStream
    {
        public Task StreamAboutTaskCreated(PopugTask task)
        {
            return Task.CompletedTask;
        }

        public Task StreamAboutTaskAssigned(PopugTask task)
        {
            return Task.CompletedTask;
        }

        public Task StreamAboutTaskClosed(PopugTask task)
        {
            return Task.CompletedTask;
        }
    }
}