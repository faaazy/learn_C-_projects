using System.ComponentModel.DataAnnotations;

namespace DotnetTodoApp.Models.Dtos;

public record RegisterDto (
    [Required]
    [StringLength(100, MinimumLength = 2)]
    string Username,

    [Required]
    [StringLength(100, MinimumLength = 8)]
    string Password
);