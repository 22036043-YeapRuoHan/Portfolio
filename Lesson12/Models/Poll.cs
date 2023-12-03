using System.ComponentModel.DataAnnotations;

namespace Lesson12.Models;
public class Poll
{
    public string? PollGUID { get; set; }

    [Required(ErrorMessage = "Question is required.")]
    [StringLength(200, ErrorMessage = "Max 200 chars")]
    public string Question { get; set; } = null!;

    [Required(ErrorMessage = "Choice A is required.")]
    [StringLength(50, ErrorMessage = "Max 50 chars")]
    public string ChoiceA { get; set; } = null!;

    [Required(ErrorMessage = "Choice B is required.")]
    [StringLength(50, ErrorMessage = "Max 50 chars")]
    public string ChoiceB { get; set; } = null!;

    public int CountA { get; set; }
    public int CountB { get; set; }

}
