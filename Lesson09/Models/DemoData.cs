using System.ComponentModel.DataAnnotations;

namespace Lesson09.Models;

public class DemoData
{
    [Required(ErrorMessage = "Please enter Date A.")]
    [DataType(DataType.Date)]
    public DateTime DateFieldA { get; set; }

    [Required(ErrorMessage = "Please enter Date B.")]
    [DataType(DataType.DateTime)]
    [DateGreaterThan("DateFieldA", ErrorMessage = "Date B must be after Date A.")]
    public DateTime DateFieldB { get; set; }

    [Required(ErrorMessage = "Please enter email.")]
    [EmailAddress(ErrorMessage = "Invalid Email")]
    public string EmailField { get; set; } = null!;

    [Range(13, 19, ErrorMessage = "Values from thirteen to nineteen only.")]
    public int Teenager { get; set; }

    [Required(ErrorMessage = "Please enter Product Code")]
    [StringLength(12, MinimumLength = 6, ErrorMessage = "6-12 characters only.")]
    public string ProductCode { get; set; } = null!;

}

