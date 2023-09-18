using _Net.Models;

namespace _Net.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetTodoItemsAsync();
        Task<TodoItem?> GetTodoItemAsync(int id);
        Task CreateTodoItemAsync(TodoItem item);
        Task<TodoItem?> UpdateTodoItemAsync(TodoItem item);
        Task<bool> DeleteTodoItemAsync(int id);
        Task<IEnumerable<TodoItem>> GetSampleTodoItemsAsync();
        Task<IEnumerable<TodoItem>> SearchTodoItemsAsync(string? searchText);

    }
}
