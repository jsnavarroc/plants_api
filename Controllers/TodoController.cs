using Microsoft.AspNetCore.Mvc;
using _Net.Models;
using _Net.Repositories;

namespace _Net.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoRepository _todoRepository;

    public TodoController(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<TodoItem>> GetTodoItems()
    {
        return await _todoRepository.GetTodoItemsAsync();
    }

    [HttpGet("{id}")]
    public ActionResult<TodoItem?> GetTodoItem(int id)
    {
        return _todoRepository.GetTodoItemAsync(id).Result;
    }

    [HttpPost]
    public ActionResult<TodoItem> CreateTodoItem(TodoItem todoItem)
    {
        _todoRepository.CreateTodoItemAsync(todoItem).Wait();
        return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTodoItem(int id, TodoItem todoItem)
    {
        if (id != todoItem.Id)
        {
            return BadRequest();
        }

        _todoRepository.UpdateTodoItemAsync(todoItem).Wait();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTodoItem(int id)
    {
        _todoRepository.DeleteTodoItemAsync(id).Wait();
        return NoContent();
    }

    [HttpGet("sample")]
    public async Task<IEnumerable<TodoItem>> GetSampleTodoItems()
    {
        return await _todoRepository.GetSampleTodoItemsAsync();
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<TodoItem>>> SearchTodoItems([FromQuery] string? searchText)
    {
        var matchingTasks = await _todoRepository.SearchTodoItemsAsync(searchText);

        if (matchingTasks.Any())
        {
            return Ok(matchingTasks);
        }
        else
        {
            return NotFound();
        }
    }

}
