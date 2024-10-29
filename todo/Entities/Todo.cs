using System.ComponentModel.DataAnnotations;

namespace todo.Entities;

public class Todo
{
    public Guid Id { get; set; }
    
    public required string Description { get; set; }

    public bool IsCompleted { get; set; }
    
}