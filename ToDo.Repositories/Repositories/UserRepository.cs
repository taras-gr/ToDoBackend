using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using ToDo.Domain;
using ToDo.Domain.Interfaces;
using ToDo.Domain.Classes;
using ToDo.Domain.Models;

namespace ToDo.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ToDoDbContext _context;

        public UserRepository(IOptions<Settings> settings)
        {
            _context = new ToDoDbContext(settings);
        }

        public async Task AddUser(User user)
        {
            try
            {
                if (await UserExistWithUserName(user.Name))
                    throw new Exception($"User with name {user.Name} is already exist.");

                await _context.Users.InsertOneAsync(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteUser(string userId)
        {
            try
            {
                DeleteResult actionResult = await _context.Users.DeleteOneAsync(Builders<User>.Filter.Eq("Id", userId));

                return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> GetUserById(ObjectId userId)
        {
            try
            {
                return await _context.Users.Find(user => user.Id == userId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> GetUserByName(string userName)
        {
            try
            {
                return await _context.Users.Find(user => user.Name == userName).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<User>> GetUsers()
        {
            try
            {
                return await _context.Users.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateUser(string userId, User user)
        {
            try
            {
                ReplaceOneResult actionResult = await _context.Users.ReplaceOneAsync(n => n.Id.ToString().Equals(userId),
                                                                                        user,
                                                                                        new UpdateOptions { IsUpsert = true });

                return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<bool> UserExistWithUserName(string userName)
        {
            var userFromRepository = await _context.Users.Find(user => user.Name == userName).FirstOrDefaultAsync();

            if (userFromRepository == null)
                return false;

            return true;
        }
    }
}