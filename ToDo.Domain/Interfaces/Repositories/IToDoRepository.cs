using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using ToDo.Domain.Models;

namespace ToDo.Domain.Interfaces
{
    public interface IToDoRepository
    {
        Task<ToDoItem> GetToDoItem(ObjectId toDoItemId);
        Task<List<ToDoItem>> GetToDoItems(string userId);
        Task AddToDoItem(ToDoItem toDoItem);
        Task<bool> UpdateToDoItem(ObjectId toDoItemId, ToDoItem toDoItem);
        Task<bool> DeleteToDoItem(ObjectId toDoItemId);
    }
}
