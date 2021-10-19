using System;
using System.Collections.Generic;
using System.Linq;

namespace RealTimeTodo.Web.Model
{
    public class TodoList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TodoItem> Items { get; set; } = new List<TodoItem>();
        public int Pending => Items.Count(p => !p.IsCompleted);
        public int Completed => Items.Count(p => p.IsCompleted);

        public TodoListMinimal GetMinimal() {
            return new TodoListMinimal() {
                Id = Id,
                Name = Name,
                Pending = Pending,
                Completed = Completed
            };
        }

        public void AddItem (string text) 
        {
            var newItemId = Items.Any() 
                ? Items.Select(i => i.Id).Max() + 1
                : 0;

            Items.Add(new TodoItem
            {
                Id = newItemId,
                Text = text
            });
        }

        public void Toggle(int itemId)
        {
            var item = Items.SingleOrDefault(i => i.Id == itemId);

            if (item == default)
            {
                throw new NullReferenceException("Item not found");
            }

            item.IsCompleted = !item.IsCompleted;
        }
    }
}