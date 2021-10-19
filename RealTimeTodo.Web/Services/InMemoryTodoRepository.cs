using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealTimeTodo.Web.Model;

namespace RealTimeTodo.Web.Services
{
    public class InMemoryTodoRepository : ITodoRepository
    {
        private static List<TodoList> Lists {get; set; } = new();

        static InMemoryTodoRepository()
        {
            Lists.Add(new TodoList { Id = 0, Name = "Foo" });
            Lists.Add(new TodoList { Id = 1, Name = "Bar" });
            Lists.Add(new TodoList { Id = 2, Name = "Test" });
            Lists.Add(new TodoList { Id = 3, Name = "Fail" });
        }

        public Task<List<TodoListMinimal>> GetLists()
        {
            return Task.FromResult(Lists
                .Select(l => l.GetMinimal())
                .ToList());
        }

        public Task<TodoList> GetList(int id)
        {
            var list = Lists.SingleOrDefault(l => l.Id == id);

            if (list == null)
            {
                throw new NullReferenceException("Invalid list id");
            }

            return Task.FromResult(Lists.SingleOrDefault(l => l.Id == id));
        }

        public async Task AddToDoItem(int listId, string text)
        {
            var list = await GetList(listId);
            list.AddItem(text);
        }

        public async Task ToggleToDoItem(int listId, int itemId)
        {
            var list = await GetList(listId);
            list.Toggle(itemId);
        }
    }
}