using System;

namespace AuthService.Host.Dto.TaskServiceDto
{
    public class PopugTaskDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public TaskUserDto Creator { get; set; }

        public TaskUserDto Assignee { get; set; }

        public DateTime Created { get; set; }

        public TaskState TaskState { get; set; }

        public string Description { get; set; }

        public DateTime? ClosedAt { get; set; }

        public decimal AssignCost { get; set; }
    }
}