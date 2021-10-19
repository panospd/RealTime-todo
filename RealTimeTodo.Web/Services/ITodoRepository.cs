using System.Collections.Generic;
using System.Threading.Tasks;
using RealTimeTodo.Web.Model;

namespace RealTimeTodo.Web.Services
{
    public interface ITodoRepository
    {
         Task<List<TodoList>> GetLists();
    }
}