using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
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

        public async Task GetLists()
        {
            var lists = await _repo.GetLists();
            await Clients.Caller.SendAsync("UpdatedToDoList", lists);
        }

        public async Task GetList(int listId)
        {
            var result = await _repo.GetList(listId);

            await Clients.Caller.SendAsync("UpdatedListData", result);
        }

        // SubscribeToCountUpdates
        public async Task SubscribeToCountUpdates()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Counts");
        }
        // UnSubscribeToCountUpdates
        public async Task UnSubscribeFromCountUpdates()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Counts");
        }
        // SubscribeToListUpdates
        public async Task SubscribeToListUpdates(int listId)
        {
            var groupName = ListIdToGroupName(listId);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
        // UnSubscribeToListUpdates
        public async Task UnSubscribeFromListUpdates(int listId)
        {
            var groupName = ListIdToGroupName(listId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
        // AddToDoItem
        public async Task AddToDoItem(int listId, string text)
        {
            await _repo.AddToDoItem(listId, text);

            var allLists = await _repo.GetLists();
            var listUpdate = await _repo.GetList(listId);

            await Clients.Group("Counts").SendAsync("UpdatedToDoList", allLists);

            var groupName = ListIdToGroupName(listId);
            await Clients.Group(groupName).SendAsync("UpdatedListData", listUpdate);
        }

        // ToggleToDoItem
        public async Task ToggleToDoItem(int listId, int itemId)
        {
            await _repo.ToggleToDoItem(listId, itemId);

            var allLists = await _repo.GetLists();
            var listUpdate = await _repo.GetList(listId);

            await Clients.Group("Counts").SendAsync("UpdatedToDoList", allLists);

            var groupName = ListIdToGroupName(listId);
            await Clients.Group(groupName).SendAsync("UpdatedListData", listUpdate);
        }

        private string ListIdToGroupName (int listId) => $"list-updates-{listId}";
    }
}