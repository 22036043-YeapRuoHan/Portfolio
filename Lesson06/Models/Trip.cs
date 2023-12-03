namespace Lesson06.Models;

public class Trip
{
    public int ID { get; set; }
    public string Title { get; set; } = null!;
    public string City { get; set; } = null!;
    public DateTime TripDate { get; set; }
    public int Duration { get; set; }
    public double Spending { get; set; }
    public string Story { get; set; } = null!;
    public string PhotoFile { get; set; } = null!;
}

