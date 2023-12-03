using System.ComponentModel.DataAnnotations;

namespace Lesson13.Models;

public class Movie

// TODO: Lesson13 Task 6 - Apply correct annotations

{
    public int MovieId { get; set; }

    [Required(ErrorMessage = "Enter a Title")]
    [StringLength(50, ErrorMessage = "Maximum is 50 Characters")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Enter the release date")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    [Range(0, 50, ErrorMessage = "0-50 dollars")]
    public double Price { get; set; }

    [Range(20, 400, ErrorMessage = "20-400 minutes")]
    public double Duration { get; set; }

    [Required(ErrorMessage = "Enter a Rating")]
    [RegularExpression("G|PG(13)?|R", ErrorMessage = "Must be G, PG, PG13 or R")]
    public string Rating { get; set; } = null!;

    [Required(ErrorMessage = "Please select Genre")]
    public int GenreId { get; set; }

    public string? GenreName { get; set; } = null;
}
// 22036043 Yeap Ruo Han