using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Lesson10.Models;

public class Performance
{
    // TODO: Lesson10 Task 3 - Write validation attributes for all fields

    [Required(ErrorMessage = "Please enter Title")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Title 1-50 chars")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Please enter an Artist")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Artist 1-50 chars")]
    public string Artist { get; set; } = null!;
    [Required(ErrorMessage = "Please enter Date/Time")]
    [DataType(DataType.DateTime)]
    [Remote(action: "VerifyDate", controller: "Performance")]
    public DateTime PerformDT { get; set; }

    [Required(ErrorMessage = "Please enter Duration")]
    [Range(0.5, 4.0, ErrorMessage = "Duration 0.5-4.0 hours")]

    public float Duration { get; set; }

    [Required(ErrorMessage = "Please enter Price")]
    [Range(0.0, 1000.0, ErrorMessage = "Price 0-1000")]

    public float Price { get; set; }

    [Required(ErrorMessage = "Please enter Chamber")]
    [RegularExpression("C[1-3][0-9]", ErrorMessage = "Invalid Chamber")]
    public string Chamber { get; set; } = null!;
}

//22036043 Yeap Ruo Han