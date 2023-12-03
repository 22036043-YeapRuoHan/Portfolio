using System.ComponentModel.DataAnnotations;

namespace Lesson09.Models;

public class TravelUser
{
    [Required(ErrorMessage = "Please enter a user ID.")]
    public string UserId { get; set; } = null!;

    [Required(ErrorMessage = "Please enter Password.")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "Password must be 5-20 characters.")]
    [DataType(DataType.Password)]
    public string UserPw { get; set; } = null!;

    [Compare("UserPw", ErrorMessage = "Passwords do not match.")]
    [DataType(DataType.Password)]
    public string UserPw2 { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a full name.")]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a birthdate.")]
    public DateTime Dob { get; set; }

    [Required(ErrorMessage = "Please enter an email.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; } = null!;

    public string UserRole { get; set; } = null!;

    public DateTime LastLogin { get; set; }

}
