using Microsoft.EntityFrameworkCore;
using TodoWebApi.Models.Entities;

namespace TodoWebApi.Models;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<TodoItem> TodoItems { get; set; } = null!;
}
