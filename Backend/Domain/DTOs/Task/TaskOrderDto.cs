namespace Backend.Domain.DTOs.Task;

public class TaskOrderDto
{
    public string? Titulo { get; set; }
    
    public DateTimeOffset? DataInicio { get; set; }
    
    public bool IsDescending { get; set; } = false;
}