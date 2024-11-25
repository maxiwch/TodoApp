
using Microsoft.EntityFrameworkCore;
using TodoWebApi.Models.Entities;

namespace TodoWebApi.Models.Repositories;

public class TodoItemRepository : IRepository
{
    private readonly TodoDbContext _context;

    public TodoItemRepository(TodoDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<TodoItem>> GetAll()
    {
        return await _context.TodoItems.ToListAsync();
    }

    public async Task<TodoItem?> GetByIdAsync(long id)
    {
        return await _context.TodoItems.FindAsync(id).AsTask();
    }

    public async Task<bool> ExistsByIdAsync(long id)
    {
        return await _context.TodoItems.AnyAsync(e => e.Id == id);
    }

    public TodoItem Insert(TodoItem entity)
    {
        return _context.TodoItems.Add(entity).Entity;
    }

    public void Update(TodoItem entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(TodoItem entity)
    {
        _context.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
