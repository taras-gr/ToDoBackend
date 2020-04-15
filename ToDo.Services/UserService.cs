using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using ToDo.Domain;
using ToDo.Domain.Interfaces;
using ToDo.Domain.Models;
using ToDo.Services.Interfaces;

namespace ToDo.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task AddUser(User user)
        {
            await _repository.AddUser(user);
        }

        public async Task<bool> DeleteUser(string userId)
        {
            return await _repository.DeleteUser(userId);
        }

        public async Task<User> GetUserById(ObjectId userId)
        {
            return await _repository.GetUserById(userId);
        }

        public async Task<User> GetUserByName(string userName)
        {
            return await _repository.GetUserByName(userName);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _repository.GetUsers();
        }

        public async Task<bool> UpdateUser(string userId, User user)
        {
            return await _repository.UpdateUser(userId, user);
        }
    }
}
