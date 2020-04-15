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
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _repository;

        public ToDoService(IToDoRepository repository)
        {
            _repository = repository;
        }
        public async Task AddToDoItem(ToDoItem toDoItem)
        {
            await _repository.AddToDoItem(toDoItem);
        }

        public async Task<bool> DeleteToDoItem(ObjectId toDoItemId)
        {
            return await _repository.DeleteToDoItem(toDoItemId);
        }

        public async Task<ToDoItem> GetToDoItem(ObjectId toDoItemId)
        {
            return await _repository.GetToDoItem(toDoItemId);
        }

        public async Task<List<ToDoItem>> GetToDoItems(string userId)
        {
            return await _repository.GetToDoItems(userId);
        }

        public async Task<bool> UpdateToDoItem(ObjectId toDoItemId, ToDoItem toDoItem)
        {
            return await _repository.UpdateToDoItem(toDoItemId, toDoItem);
        }
    }
}
