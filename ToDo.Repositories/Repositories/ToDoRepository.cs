using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using ToDo.Domain;
using ToDo.Domain.Classes;
using ToDo.Domain.Interfaces;
using ToDo.Domain.Models;

namespace ToDo.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ToDoDbContext _context;

        public ToDoRepository(IOptions<Settings> settings)
        {
            _context = new ToDoDbContext(settings);
        }

        public async Task AddToDoItem(ToDoItem toDoItem)
        {
            try
            {
                await _context.ToDoItems.InsertOneAsync(toDoItem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteToDoItem(ObjectId toDoItemId)
        {
            try
            {
                DeleteResult actionResult = await _context.ToDoItems.DeleteOneAsync(Builders<ToDoItem>.Filter.Eq("Id", toDoItemId));

                return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ToDoItem> GetToDoItem(ObjectId toDoItemId)
        {
            try
            {
                return await _context.ToDoItems.Find(toDoItem => toDoItem.Id == toDoItemId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ToDoItem>> GetToDoItems(string userId)
        {
            try
            {
                return await _context.ToDoItems.Find(toDoItem => toDoItem.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateToDoItem(ObjectId toDoItemId, ToDoItem toDoItem)
        {
            try
            {
                ReplaceOneResult actionResult = await _context.ToDoItems.ReplaceOneAsync(n => n.Id.Equals(toDoItemId),
                                                                                            toDoItem,
                                                                                            new UpdateOptions { IsUpsert = true });

                return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
