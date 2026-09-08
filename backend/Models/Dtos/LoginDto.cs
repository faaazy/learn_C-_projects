using System.ComponentModel.DataAnnotations;

namespace DotnetTodoApp.Models.Dtos;

public record LoginDto(
    [Required]
    string Username,

    [Required]
    string Password
);