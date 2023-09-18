using Microsoft.EntityFrameworkCore;
using _Net.Data;
using _Net.Models;
using _Net.Repositories;
using NUnit.Framework;

namespace _Net.Tests
{
    public class TodoRepositoryTests
    {
        private ApiDBContext _context = new ApiDBContext(new DbContextOptions<ApiDBContext>());
        private TodoRepository _todoRepository = new TodoRepository(new ApiDBContext(new DbContextOptions<ApiDBContext>()));

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApiDBContext>()
                .UseInMemoryDatabase("TodoDB")
                .Options;

            _context = new ApiDBContext(options);
            _todoRepository = new TodoRepository(_context);

        }

        [Test]
        public async Task TestGetTodoItemsAsync_ShouldReturnAllTodoItems()
        {
            var todoItem1 = new TodoItem { Title = "Regar las plantas", Description = "Riego diario", Img = new ImageInfo { Src = "imagen1.jpg", Alt = "Imagen 1" } };
            var todoItem2 = new TodoItem { Title = "Limpiar la casa", Description = "Limpieza semanal", Img = new ImageInfo { Src = "imagen2.jpg", Alt = "Imagen 2" } };
            _context.TodoItems.Add(todoItem1);
            _context.TodoItems.Add(todoItem2);
            await _context.SaveChangesAsync();

            var todoItems = await _todoRepository.GetTodoItemsAsync();

            var actualTodoItems = todoItems.ToList();
            Assert.AreEqual(2, actualTodoItems.Count());
            Assert.Contains(todoItem1, actualTodoItems);
            Assert.Contains(todoItem2, actualTodoItems);
        }

        [Test]
        public async Task TestGetTodoItemAsync_ShouldReturnTodoItemWithGivenId()
        {
            var todoItem = new TodoItem { Title = "Regar las plantas", Description = "Riego diario", Img = new ImageInfo { Src = "imagen1.jpg", Alt = "Imagen 1" }, Id = 1 };
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            var todoItemFromRepository = await _todoRepository.GetTodoItemAsync(1);

            Assert.NotNull(todoItemFromRepository);
            Assert.AreEqual(todoItem.Title, todoItemFromRepository?.Title);
            Assert.AreEqual(todoItem.Id, todoItemFromRepository?.Id);
            Assert.AreEqual(todoItem.Description, todoItemFromRepository?.Description);
            Assert.AreEqual(todoItem.Img.Src, todoItemFromRepository?.Img?.Src);
            Assert.AreEqual(todoItem.Img.Alt, todoItemFromRepository?.Img?.Alt);
        }

        [Test]
        public async Task TestUpdateTodoItemAsync_ShouldUpdateTodoItem()
        {
            var todoItem = new TodoItem { Title = "Regar las plantas", Description = "Riego diario", Img = new ImageInfo { Src = "imagen1.jpg", Alt = "Imagen 1" }, Id = 1 };
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            var updatedTodoItem = new TodoItem { Title = "Regar las plantas y podar las flores", Description = "Riego y poda", Img = new ImageInfo { Src = "imagen3.jpg", Alt = "Imagen 3" }, Id = 1 };

            await _todoRepository.UpdateTodoItemAsync(updatedTodoItem);

            var todoItemFromRepository = await _todoRepository.GetTodoItemAsync(1);
            Assert.NotNull(todoItemFromRepository);
            Assert.AreEqual(updatedTodoItem.Title, todoItemFromRepository?.Title);
            Assert.AreEqual(updatedTodoItem.Description, todoItemFromRepository?.Description);
            Assert.AreEqual(updatedTodoItem.Img.Src, todoItemFromRepository?.Img.Src);
            Assert.AreEqual(updatedTodoItem.Img.Alt, todoItemFromRepository?.Img.Alt);
        }

        [Test]
        public async Task TestDeleteTodoItemAsync_ShouldDeleteTodoItem()
        {
            var todoItem = new TodoItem { Title = "Regar las plantas", Description = "Riego diario", Img = new ImageInfo { Src = "imagen1.jpg", Alt = "Imagen 1" }, Id = 1 };
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            var deleted = await _todoRepository.DeleteTodoItemAsync(1);

            Assert.True(deleted);
            Assert.False(_context.TodoItems.Any(x => x.Id == 1));
        }
    }
}
