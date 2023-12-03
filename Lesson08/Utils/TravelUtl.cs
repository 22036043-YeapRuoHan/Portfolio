namespace RP.SOI.DotNet.Utils;

public static class TravelUtl
{
    public static string Abbreviate(this string story)
    {
        return story.Length < 30 ? story : story[..20] + "...";
    }
}
