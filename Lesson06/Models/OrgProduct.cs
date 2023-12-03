namespace Lesson06.Models;

public class OrgProduct
{
    public int OrgCode { get; set; }
    public string OrgDesc { get; set; } = null!;
    public double Price { get; set; }
    public int Gram { get; set; }
    public string Country { get; set; } = null!;
}
