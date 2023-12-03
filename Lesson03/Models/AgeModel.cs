namespace Lesson03.Models;

public class AgeModel
{
    public DateTime DobMary { get; set; }
    public DateTime DobJohn { get; set; }
    public DateTime DobMike { get; set; }
    public DateTime DobPaul { get; set; }

    public AgeModel(DateTime dobMary, DateTime dobJohn, DateTime dobMike, DateTime dobPaul)
    {
        DobMary = dobMary;
        DobJohn = dobJohn;
        DobMike = dobMike;
        DobPaul = dobPaul;
    }
}
