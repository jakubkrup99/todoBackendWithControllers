using Microsoft.EntityFrameworkCore;

namespace todo.Entities;

public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
{
    public DbSet<Todo> Todos { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>()
            .Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Todo>()
            .Property(t => t.IsCompleted)
            .IsRequired();
    }
}