using System.ComponentModel.DataAnnotations;

namespace Lesson08.Models;

public class UserLogin
{
    [Required(ErrorMessage = "Please enter User ID")]
    public string UserID { get; set; } = null!;

    [Required(ErrorMessage = "Please enter Password")]
    public string Password { get; set; } = null!;
}
