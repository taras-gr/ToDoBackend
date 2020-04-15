using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApi.DTOs
{
    public class ToDoItemCreateDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
    }
}
