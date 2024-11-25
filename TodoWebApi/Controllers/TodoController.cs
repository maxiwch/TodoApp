using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using TodoWebApi.Models.DTOs;
using TodoWebApi.Models.Entities;
using TodoWebApi.Services;

namespace TodoWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    // GET: api/todo
    [HttpGet(Name = nameof(GetAllTodos))]
    public async Task<ActionResult<List<TodoItem>>> GetAllTodos()
    {
        return await _todoService.GetAllAsync();
    }

    // POST: api/todo
    [HttpPost(Name = nameof(CreateTodo))]
    public async Task<ActionResult<TodoItem>> CreateTodo(NewTodoRequest newTodo)
    {
        var todoItem = await _todoService.InsertAsync(newTodo);
        return CreatedAtAction("GetTodo", new { id = todoItem.Id }, todoItem);
    }

    // GET: api/todo/1
    [HttpGet("{id:long}", Name = nameof(GetTodo))]
    public async Task<ActionResult<TodoItem>> GetTodo(long id)
    {
        var todoItem = await _todoService.GetByIdAsync(id);
        if (todoItem == null)
        {
            return NotFound();
        }
        return todoItem;
    }

    // PUT: api/todo/1
    [HttpPut("{id:long}", Name = nameof(UpdateTodo))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTodo(long id, UpdateTodoRequest todoItem)
    {
        if (id != todoItem.Id)
        {
            return BadRequest();
        }
        var updated = await _todoService.UpdateAsync(todoItem);
        return updated ? Ok() : NotFound();
    }

    // DELETE: api/todo/1
    [HttpDelete("{id:long}", Name = nameof(DeleteTodo))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTodo(long id)
    {
        var deleted = await _todoService.DeleteById(id);
        return deleted ? NoContent() : NotFound();
    }
}
