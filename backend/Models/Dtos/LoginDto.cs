using System.ComponentModel.DataAnnotations;

namespace plzwork.Models.Dtos;

public record LoginDto(
    [Required]
    string Username,

    [Required]
    string Password
);