using System.Collections.Generic;
using System.Threading.Tasks;
using RealTimeTodo.Web.Model;

namespace RealTimeTodo.Web.Services
{
    public class InMemoryTodoRepository : ITodoRepository
    {
        private static List<TodoList> Lists {get; set; } = new();
        public Task<List<TodoList>> GetLists()
        {
            return Task.FromResult(Lists);
        }
    }
}