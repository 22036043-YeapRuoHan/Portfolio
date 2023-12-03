using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Lesson12.Models;

public class Warehouse
{
    [Required(ErrorMessage = "ID mandatory")]
    [RegularExpression("(([1-6][0-9])|7[0-8])[0-9]",ErrorMessage = "Id must be 100-789")]
    
    public int Id { get; set; }
    [Required(ErrorMessage = "Field is mandatory")]
    [StringLength(21,  ErrorMessage = "Maximum 21 Characters")]
    public string Alias { get; set; } = null!;

    public string Phone { get; set; } = null!;
    [Required(ErrorMessage = "Please enter a valid address")]
    [StringLength(38, ErrorMessage = "Maximum 38 Characters")]
    public string AddressLine1 { get; set; } = null!;
    [StringLength(38, ErrorMessage = "Maximum 38 Characters")]
    public string AddressLine2 { get; set; } = null!;
    [Required(ErrorMessage = "City name must be entered")]
    public string City { get; set; } = null!;
    [Required(ErrorMessage = "State abbreviation required")]
    [RegularExpression("(QLD|NSW|VIC)", ErrorMessage = "Valid 3-character state abbreviations only")]
    public string State { get; set; } = null!;
    [Required(ErrorMessage = "Please enter an Australia Post postcode")]
    [RegularExpression("[2-3][0-9][0-9][0-9]|4[1-5][0-6]0", ErrorMessage = "Postcode must be 2000-4560")]
    public int Postcode { get; set; }
}
