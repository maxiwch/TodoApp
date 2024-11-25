using TodoWebApi.Models.Entities;

namespace TodoWebApi.Models.Repositories;

public interface IRepository
{
    Task<TodoItem?> GetByIdAsync(long id);
    Task<bool> ExistsByIdAsync(long id);
    Task<List<TodoItem>> GetAll();
    TodoItem Insert(TodoItem entity);
    void Update(TodoItem entity);
    void Delete(TodoItem entity);
    Task SaveChangesAsync();
}
