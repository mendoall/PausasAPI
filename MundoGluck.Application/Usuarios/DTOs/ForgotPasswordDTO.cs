using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MundoGluck.Application.Usuarios.DTOs;
public class ForgotPasswordDTO
{
    [Required]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string Email { get; set; } = string.Empty;

    public string? ResetToken { get; set; } = string.Empty;
}
