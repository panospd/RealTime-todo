using System.Collections.Generic;

namespace RealTimeTodo.Web.Model
{
    public class TodoList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TodoItem> Items { get; set; }
    }
}