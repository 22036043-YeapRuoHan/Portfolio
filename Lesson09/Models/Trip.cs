using System.ComponentModel.DataAnnotations;

namespace Lesson09.Models;

public class Trip
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter a title.")]
    [StringLength(100, ErrorMessage = "Max 100 chars")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a city.")]
    [StringLength(70, ErrorMessage = "Max 70 chars")]
    public string City { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a date.")]
    [DataType(DataType.Date)]
    public DateTime TripDate { get; set; }

    [Range(1, 365, ErrorMessage = "Duration must be 1-365 days.")]
    public int Duration { get; set; }

    [Range(0, 100000, ErrorMessage = "Spending must be $1-100K dollars.")]
    public double Spending { get; set; }

    [Required(ErrorMessage = "Please enter a story")]
    [StringLength(2000, ErrorMessage = "Max 2000 characters.")]
    public string Story { get; set; } = null!;

    [Required(ErrorMessage = "Please select a photo.")]
    public IFormFile Photo { get; set; } = null!;

    public string Picture { get; set; } = null!;

    public string SubmittedBy { get; set; } = null!;

}

