using System;

namespace TasksService.BO
{
    public class PopugTask
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        
        public User Creator { get; set; }
        
        public User Assignee { get; set; }
        
        public DateTime Created { get; set; }
        
        public TaskState TaskState { get; set; }
        
        public string Description { get; set; }

        public DateTime? ClosedAt { get; set; }

        /// <summary>
        /// Стоимость задачи, чтобы взять
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Награда за закрытие задачи
        /// </summary>
        public decimal? Reward { get; set; }

        private DateTime jiraStartDate = new DateTime(2021, 3, 3);
        private static Random costGenerator = new Random(Guid.NewGuid().GetHashCode());
        private static double maxCost = 100;
        private static double minCost = 10;

        private void Validate()
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

        private void SetAssignCost()
        {
            Cost = Convert.ToDecimal(costGenerator.NextDouble() * (maxCost - minCost) + minCost);
        }

        public static PopugTask CreateNew(PopugTask template, User creator)
        {
            template.TaskState = TaskState.Open;
            template.Created = DateTime.Now;
            template.Creator = creator;
            template.Validate();
            template.SetAssignCost();

            return template;
        }

        public PopugTask AssignToRandomUser(UsersCollection users)
        {
            Assignee = users.GetRandomUser();;
            return this;
        }

        public PopugTask Close(User whoIsClosing)
        {
            if (whoIsClosing.Id != Assignee.Id)
                throw new ArgumentException($"{whoIsClosing.UserName} cannot close task because he or she doesn't own it");

            ClosedAt = DateTime.Now;
            TaskState = TaskState.Done;
            Reward = Convert.ToDecimal(costGenerator.NextDouble() * (maxCost - minCost) + minCost);
            return this;
        }
    }
}