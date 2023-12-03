using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Lesson10.Models;

public class Email
{
    [Required(ErrorMessage = "A customer name is required.")]
    public string CustomerName { get; set; } = null!;

    [Required(ErrorMessage = "A customer email address is required.")]
    [EmailAddress(ErrorMessage = "Email address is not valid.")]
    public string CustomerEmail { get; set; } = null!;

    [Required(ErrorMessage = "A subject is required.")]
    public string Subject { get; set; } = null!;

    [Required(ErrorMessage = "Message cannot be null.")]
    public string Message { get; set; } = null!;

    //[Remote("VerifyEmail", "Users")]  The Users class is not included.
    public string Vmail { get; set; } = null!;
}

