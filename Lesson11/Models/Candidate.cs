using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Lesson11.Models;

public class Candidate
{
    [Required(ErrorMessage = "Please enter Reg #")]
    [Range(100, 999, ErrorMessage = "Reg # 100 - 999")]
    public int RegNo { get; set; }

    [Required(ErrorMessage = "Please enter Name")]
    [StringLength(50, ErrorMessage = "Please enter Name")]
    public string CName { get; set; } = null!;

    [Required(ErrorMessage = "Please enter Gender")]
    [RegularExpression("[MF]", ErrorMessage = "Invalid Gender")]
    public string Gender { get; set; } = null!;

    [Range(1.65, 3.00, ErrorMessage = "Height must be 1.65 and above")]
    public double Height { get; set; }

    [Remote(action: "CheckDOB", controller: "Candidate")]
    public DateTime BirthDate { get; set; }

    [Required(ErrorMessage = "Please enter Race")]
    [RegularExpression("CH|MA|IN|OT", ErrorMessage = "Invalid Race")]
    public string Race { get; set; } = null!;

    public bool Clearance { get; set; }
}
