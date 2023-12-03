using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Lesson10.Models;

public class VData
{
    [Required(ErrorMessage = "A Go Time is required.")]
    [RegularExpression(@"([01]\d|2[0-3]):[0-5]\d",
        ErrorMessage = "Go Time must match pattern for a time HH24:MI")]
    public string GoTime { get; set; } = null!;

    [Remote("CheckBalance", "Demo",
        ErrorMessage = "Overwrite Server, Yes?")]
    public double Amount { get; set; }

    [Remote("CheckOrder1", "Demo",
            AdditionalFields = "Second,Third")]
    public int First { get; set; }

    [Remote("CheckOrder2", "Demo",
            AdditionalFields = "First,Third")]
    public int Second { get; set; }

    [Remote("CheckOrder3", "Demo",
            AdditionalFields = "First,Second")]
    public int Third { get; set; }

    [Remote(action: "CheckDates", controller: "Demo",
            AdditionalFields = nameof(End))]
    [DataType(DataType.Date)]
    public DateTime Begin { get; set; }

    [Remote(action: "CheckDates", controller: "Demo",
            AdditionalFields = nameof(Begin))]
    [DataType(DataType.Date)]
    public DateTime End { get; set; }
}
