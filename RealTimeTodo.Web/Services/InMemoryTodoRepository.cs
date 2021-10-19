using System.Collections.Generic;
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

        public Task<List<TodoList>> GetLists()
        {
            return Task.FromResult(Lists);
        }
    }
}