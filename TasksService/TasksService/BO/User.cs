using System;
using LinqToDB.Mapping;

namespace TasksService.BO
{
    [Table(Name = "users")]
    public class User
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        
        [NotNull]
        [Column]
        public string Name { get; set; }
    }
}