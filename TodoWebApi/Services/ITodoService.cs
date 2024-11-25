using TodoWebApi.Models.DTOs;
using TodoWebApi.Models.Entities;

namespace TodoWebApi.Services;

public interface ITodoService
{
    Task<TodoItem?> GetByIdAsync(long id);
    Task<List<TodoItem>> GetAllAsync();
    Task<TodoItem> InsertAsync(NewTodoRequest entity);
    Task<bool> UpdateAsync(UpdateTodoRequest entity);
    Task<bool> DeleteById(long id);
}
