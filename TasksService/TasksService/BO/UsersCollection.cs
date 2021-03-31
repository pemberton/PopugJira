using System;
using System.Collections.Generic;

namespace TasksService.BO
{
    public class UsersCollection
    {
        private static Random random = new Random(Guid.NewGuid().GetHashCode());

        public UsersCollection(List<User> users)
        {
            Users = users ?? throw new ArgumentNullException(nameof(users));
        }

        public List<User> Users { get; set; }

        public User GetRandomUser()
        {
            var userIndex = random.Next(0, Users.Count);
            return Users[userIndex];
        }
    }
}