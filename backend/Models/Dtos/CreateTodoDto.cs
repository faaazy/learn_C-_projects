using System.ComponentModel.DataAnnotations;

namespace DotnetTodoApp.Models.Dtos;

public record CreateTodoDto(
    [Required]
    [StringLength(100, MinimumLength = 3)]
    string Name, 

    [Required]
    DateTime? DueDate
);