namespace Lesson04.Models;

public class Candidate
{
    public int RegNo { get; set; }
    public string Name { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public bool Clearance { get; set; }
}
