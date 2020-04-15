using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using ToDo.Domain.Models;

namespace ToDo.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserById(ObjectId userId);
        Task<User> GetUserByName(string userName);
        Task<List<User>> GetUsers();
        Task AddUser(User user);
        Task<bool> UpdateUser(string userId, User user);
        Task<bool> DeleteUser(string userId);
    }
}
