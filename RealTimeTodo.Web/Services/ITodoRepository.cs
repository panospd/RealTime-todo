using System.Collections.Generic;
using System.Threading.Tasks;
using RealTimeTodo.Web.Model;

namespace RealTimeTodo.Web.Services
{
    public interface ITodoRepository
    {
         Task<List<TodoListMinimal>> GetLists();
         Task<TodoList> GetList(int id);
         Task AddToDoItem(int listId, string text);
         Task ToggleToDoItem (int listId, int itemId);
    }
}