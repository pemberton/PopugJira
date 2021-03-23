using System;
using LinqToDB.Mapping;
using TasksService.BO;

namespace TasksService.Db
{
    [Table(Name = "Tasks")]
    public class PopugTaskDb
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [NotNull]
        [Column]
        public string Title { get; set; }

        [NotNull]
        [Column]
        public Guid CreatorId { get; set; }
        
        [Association(ThisKey="CreatorId", OtherKey="Id", CanBeNull=false)]
        public User Creator { get; set; }

        [NotNull]
        [Column(Name="Assignee")]
        public Guid? AssigneeId { get; set; }

        [Association(ThisKey="AssigneeId", OtherKey="Id", CanBeNull=true)]
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

    }
}