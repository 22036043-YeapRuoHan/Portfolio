using System.ComponentModel.DataAnnotations;

namespace Lesson08.Models;

public class Candidate
{
    
    public int RegNo { get; set; }                    // int's Default is [Required]
    [Required]
    public string CName { get; set; } = null!;
    [Required]
    public string Gender { get; set; } = null!;
    public double Height { get; set; }                // double's Default is [Required]
    public DateTime BirthDate { get; set; }           // DateTime's Default is [Required]
    [Required]
    public string Race { get; set; } = null!;
    public bool Clearance { get; set; }               // bool's Default is [False]
    [Required]
    public IFormFile Photo { get; set; } = null!;
    public string? PicFile { get; set; } = null;      // PicFile derives from Photo
}

//
// Sample below is BEFORE annotations and before addition of 
// null-forgiving operator.
//
//public class Candidate
//{
//   public int RegNo { get; set; }           
//   public string Name { get; set; }         
//   public string Gender { get; set; }       
//   public double Height { get; set; }       
//   public DateTime BirthDate { get; set; }  
//   public string Race { get; set; }         
//   public bool Clearance { get; set; }      
//   public string? PicFile { get; set; }      
//}
