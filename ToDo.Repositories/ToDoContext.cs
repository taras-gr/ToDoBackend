using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using ToDo.Domain;
using ToDo.Domain.Classes;
using ToDo.Domain.Models;

namespace ToDo.Repositories
{
    public class ToDoDbContext
    {
        private readonly IMongoDatabase _database;

        public ToDoDbContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<ToDoItem> ToDoItems => _database.GetCollection<ToDoItem>("ToDoItems");

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    }
}
