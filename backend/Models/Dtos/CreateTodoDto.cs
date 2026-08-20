using System.ComponentModel.DataAnnotations;

namespace plzwork.Models.Dtos;

public record CreateTodoDto(
    [Required]
    [StringLength(100, MinimumLength = 3)]
    string Name, 
    DateTime DueDate);