using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using todo.Dtos;
using todo.Entities;
using todo.Services;

namespace todo.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController(ITodoService todoService) : ControllerBase
{
    private readonly ITodoService _todoService = todoService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Todo>>> GetAllAsync()
    {
        var results = await _todoService.GetAllAsync();
        return Ok(results);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Todo>> GetAsync([FromRoute] Guid id)
    {
        var result = await _todoService.GetByIdAsync(id);
        
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Todo>> PostAsync([FromBody] TodoDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await _todoService.AddAsync(dto);
        return Created($"todo/{result.Id}", result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var isDeleted = await _todoService.DeleteAsync(id);
        
        return isDeleted ? NoContent() : NotFound();
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> PatchAsync([FromRoute] Guid id, [FromBody] JsonPatchDocument<Todo> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest();
        }
        var result = await _todoService.PatchAsync(id, patchDoc);
        if (result is null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }
    
}