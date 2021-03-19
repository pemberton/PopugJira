using System;
using System.Threading.Channels;
using LinqToDB.Mapping;

namespace TasksService.BO
{
    [Table(Name = "Tasks")]
    public class PopugTask
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [NotNull]
        [Column]
        public string Title { get; set; }
        
        [Association(ThisKey="creator", OtherKey="id", CanBeNull=false)]
        public User Creator { get; set; }
        
        [Nullable]
        [Column]
        public User Assignee { get; set; }
        
        [NotNull]
        [Column]
        public DateTime Created { get; set; }
        
        [NotNull]
        [Column]
        public TaskState TaskState { get; set; }
        
        [NotNull]
        [Column]
        public string Description { get; set; }

        [Nullable]
        [Column]
        public DateTime? ClosedAt { get; set; }

        private DateTime jiraStartDate = new DateTime(2021, 3, 3);
        
        public void Validate()
        {
            if (string.IsNullOrEmpty(Title))
                throw new ArgumentException("You should provide task title");
            if (Creator == null)
                throw new ArgumentException("You should provide creator");
            if (Created < jiraStartDate)
                throw new ArgumentException("Created should be greater than March 3, 2021");
            if (ClosedAt.HasValue && ClosedAt < jiraStartDate)
                throw new ArgumentException("ClosedAt should be greater than March 3, 2021");
            if (string.IsNullOrEmpty(Description))
                throw new ArgumentException("You should provide task description");
        }
    }
}