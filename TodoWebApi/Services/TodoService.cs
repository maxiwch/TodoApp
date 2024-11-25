using Microsoft.EntityFrameworkCore;
using TodoWebApi.Models.DTOs;
using TodoWebApi.Models.Entities;
using TodoWebApi.Models.Repositories;

namespace TodoWebApi.Services;

public class TodoService : ITodoService
{
    private readonly IRepository _todoRepository;

    public TodoService(IRepository todoRepository)
    {
        _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
    }

    public async Task<List<TodoItem>> GetAllAsync()
    {
        return await _todoRepository.GetAll();
    }

    public async Task<TodoItem?> GetByIdAsync(long id)
    {
        return await _todoRepository.GetByIdAsync(id);
    }

    public async Task<TodoItem> InsertAsync(NewTodoRequest request)
    {
        ArgumentNullException.ThrowIfNull(request.Content, nameof(request.Content));
        ArgumentNullException.ThrowIfNull(request.EndDate, nameof(request.EndDate));

        var entity = new TodoItem(request.Content, request.EndDate.Value);
        var result = _todoRepository.Insert(entity);
        await _todoRepository.SaveChangesAsync();
        return result;
    }

    public async Task<bool> UpdateAsync(UpdateTodoRequest request)
    {
        ArgumentNullException.ThrowIfNull(request.Id);
        ArgumentNullException.ThrowIfNull(request.Content);
        ArgumentNullException.ThrowIfNull(request.IsCompleted);
        ArgumentNullException.ThrowIfNull(request.EndDate);

        var entity = new TodoItem(request.Content, request.EndDate.Value)
        {
            Id = request.Id.Value,
            IsCompleted = request.IsCompleted.Value
        };
        _todoRepository.Update(entity);

        try
        {
            await _todoRepository.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            var exists = await _todoRepository.ExistsByIdAsync((long)entity.Id);
            if (!exists)
            {
                return false;
            }
            throw;
        }
    }

    public async Task<bool> DeleteById(long id)
    {
        var entity = await _todoRepository.GetByIdAsync(id);
        if (entity == null)
        {
            return false;
        }
        _todoRepository.Delete(entity);
        await _todoRepository.SaveChangesAsync();
        return true;
    }
}
