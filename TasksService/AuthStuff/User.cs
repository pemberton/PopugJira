using LinqToDB.Mapping;

namespace AuthStuff
{
    [Table(Name="Users")]
    public class User
    {
        [Column]
        [PrimaryKey]
        [NotNull]
        public string Id { get; set; }

        [Column]
        [NotNull]
        public string Name { get; set; }

        [Column]
        [Nullable]
        public string Role { get; set; }
    }
}