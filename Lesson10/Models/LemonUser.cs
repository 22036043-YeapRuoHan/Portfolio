using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Lesson10.Models;

public class LemonUser
{
    // TODO: Lesson10 Task 1 - Fill in the blanks and uncomment to enable the validation attributes.

    [Required(ErrorMessage = "Please enter a user Id.")]
    [Remote(action: "VerifyUserID", controller: "Account")]
    public string UserId { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a password.")]
    [DataType(DataType.Password)] 
    [StringLength(20, MinimumLength = 5, ErrorMessage = "Password must at least 5 characters.")]
    public string UserPw { get; set; } = null!;

    [Compare("UserPw", ErrorMessage = "Passwords do not match.")]
    [DataType(DataType.Password)]
    public string UserPw2 { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a full name.")]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "Please enter an email address.")]
    [EmailAddress(ErrorMessage = "Email address is not valid.")]
    public string Email { get; set; } = null!;

    public string UserRole { get; set; } = null!;
    public DateTime LastLogin { get; set; }
}

//22036043 Yeap Ruo Han