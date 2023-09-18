using Microsoft.EntityFrameworkCore;
using _Net.Data;
using _Net.Models;
using _Net.TestData;
using System.Text.RegularExpressions;

namespace _Net.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        protected readonly ApiDBContext _context;

        public TodoRepository(ApiDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync()
        {
            var todoItems = await _context.TodoItems.Include(item => item.Img).ToListAsync();
            return todoItems;
        }

        public async Task<TodoItem?> GetTodoItemAsync(int id)
        {
            var todoItems = await _context.TodoItems.Include(item => item.Img).ToListAsync();
            return todoItems.FirstOrDefault(x => x.Id == id);
        }

        public async Task CreateTodoItemAsync(TodoItem item)
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<TodoItem?> UpdateTodoItemAsync(TodoItem item)
        {
            try
            {
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteTodoItemAsync(int id)
        {
            // The return type is now `Task`.
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem is not null)
            {
                _context.TodoItems.Remove(todoItem);

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<IEnumerable<TodoItem>> GetSampleTodoItemsAsync()
        {
            var sampleTasks = await SampleTodoItems.GetSampleTasksAsync();
            foreach (var task in sampleTasks)
            {
                await _context.AddAsync(task);
            }
            await _context.SaveChangesAsync();
            var savedTasks = await GetTodoItemsAsync();
            return savedTasks;
        }
        private string CleanAndNormalizeText(string? input)
        {
            if (input == null)
            {
                return string.Empty;
            }

            // Convertir a minúsculas, eliminar espacios en blanco y caracteres especiales
            return Regex.Replace(input.ToLowerInvariant(), "[^a-zA-Z0-9]", "");
        }
        public async Task<IEnumerable<TodoItem>> SearchTodoItemsAsync(string? searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return await GetTodoItemsAsync();
            }
            string cleanSearchText = CleanAndNormalizeText(searchText);


            var matchingTodoItems = await _context.TodoItems
                .Include(item => item.Img)
                .Where(item =>
                    (item.Title != null && CleanAndNormalizeText(item.Title).Contains(cleanSearchText)) ||
                    (item.Description != null && CleanAndNormalizeText(item.Description).Contains(cleanSearchText)))
                .ToListAsync();


            return matchingTodoItems;
        }
    }

}
