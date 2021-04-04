using System;
using System.Linq;
using System.Threading.Tasks;
using AuthStuff.Db;
using LinqToDB;

namespace AuthStuff
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDbUsersProvider _dbUsersProvider;

        public UsersRepository(IDbUsersProvider dbUsersProvider)
        {
            _dbUsersProvider = dbUsersProvider ?? throw new ArgumentNullException(nameof(dbUsersProvider));
        }

        public async Task AddUser(User user)
        {
            await using var db = _dbUsersProvider.GetDbUsers();
            await db.InsertAsync(user);
        }

        public async Task AddRoleToUser(string userId, string roleName)
        {
            await using var db = _dbUsersProvider.GetDbUsers();
            await db.Users
                .Where(p => p.Id == userId)
                .Set(p => p.Role, roleName)
                .UpdateAsync();
        }
    }
}