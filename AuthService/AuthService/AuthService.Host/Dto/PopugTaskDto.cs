using System;

namespace AuthService.Host.Dto
{
    public class PopugTaskDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public UserGetAllDto Creator { get; set; }

        public UserGetAllDto Assignee { get; set; }

        public DateTime Created { get; set; }

        public TaskState TaskState { get; set; }

        public string Description { get; set; }

        public DateTime? ClosedAt { get; set; }
    }
}