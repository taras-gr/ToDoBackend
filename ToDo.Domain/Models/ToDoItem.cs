using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDo.Domain.Models
{
    public class ToDoItem
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
        public string UserId { get; set; }
    }
}