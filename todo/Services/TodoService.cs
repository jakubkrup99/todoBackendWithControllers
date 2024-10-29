using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using todo.Dtos;
using todo.Entities;

namespace todo.Services;

public interface ITodoService
{
    Task<IEnumerable<Todo>> GetAllAsync();
    Task<Todo?> GetByIdAsync(Guid id);
    Task<Todo> AddAsync(TodoDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<Todo?> PatchAsync(Guid id, JsonPatchDocument<Todo> patchDoc);
}

public class TodoService(TodoDbContext dbContext) : ITodoService
{
    private readonly TodoDbContext _dbContext = dbContext;

    public async Task<IEnumerable<Todo>> GetAllAsync()
    {
        var results = await _dbContext.Todos.Select(t => t).AsNoTracking().ToListAsync();
        return results;
    }

    public async Task<Todo?> GetByIdAsync(Guid id)
    {
        var result = await _dbContext.Todos.FindAsync(id);

        return result;

    }

    public async Task<Todo> AddAsync(TodoDto dto)
    {
        Todo result = new Todo() { Description = dto.Description, IsCompleted = dto.IsCompleted };
        await _dbContext.Todos.AddAsync(result);
        await _dbContext.SaveChangesAsync();
        return result;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var rowsDeleted = await _dbContext.Todos.Where(t => t.Id == id).ExecuteDeleteAsync();
        
        return rowsDeleted > 0;
    }

    public async Task<Todo?> PatchAsync(Guid id, JsonPatchDocument<Todo> patchDoc)
    {
        var existingItem = await _dbContext.Todos.FindAsync(id);
        if (existingItem is null)
        {
            return null;
        }
        patchDoc.ApplyTo(existingItem);
        await _dbContext.SaveChangesAsync();
        return existingItem;
    }
    
}