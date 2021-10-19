using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RealTimeTodo.Web.Model;
using RealTimeTodo.Web.Services;

namespace RealTimeTodo.Web.Hubs
{
    public class TodoHub : Hub
    {
        private readonly ITodoRepository _repo;

        public TodoHub(ITodoRepository repo)
        {
            _repo = repo;
        }

        public Task<List<TodoList>> GetLists()
        {
            return _repo.GetLists();
        }
    }
}