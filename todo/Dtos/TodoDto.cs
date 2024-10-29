using System.ComponentModel.DataAnnotations;

namespace todo.Dtos;

public record TodoDto
{
    [Required]
    [MaxLength(100)]
    public required string Description { get; init; }
    [Required]
    public bool IsCompleted { get; init; }

   
}