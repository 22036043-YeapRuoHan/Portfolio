using System.ComponentModel.DataAnnotations;

namespace Lesson08.Models;

public class Trip
{
    // TODO Lesson 08 Task 3 - Specify [Required] for some properties

    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public string City { get; set; } = null!;
    [DataType(DataType.Date)]
    public DateTime TripDate { get; set; }
    public int Duration { get; set; }
    public double Spending { get; set; }
    [Required]
    public string Story { get; set; } = null!;
    [Required]
    public IFormFile Photo { get; set; } = null!;
    [Required]
    public string Picture { get; set; } = null!;
    [Required]
    public string SubmittedBy { get; set; } = null!;
}
// 22036043 Yeap Ruo Han