﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinqToDB;
using TasksService.BO;
using TasksService.Db;
using TasksService.Repositories.Contracts;

namespace TasksService.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDbProvider _dbProvider;

        public UsersRepository(IDbProvider dbProvider)
        {
            _dbProvider = dbProvider ?? throw new ArgumentNullException(nameof(dbProvider));
        }

        public async Task<User> GetById(Guid userId)
        {
            await using var db = _dbProvider.GetDbPopugJira();
            
            var record = await db.Users
                .SingleOrDefaultAsync(s => s.Id == userId);

            return record;
        }
    }
}