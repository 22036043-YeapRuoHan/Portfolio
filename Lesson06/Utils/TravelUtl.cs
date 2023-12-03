namespace RP.SOI.DotNet.Utils;

public static class TravelUtl
{
    public static string Abbreviate(this string story)
    {
        if (story.Length < 30)
        {
            return story;
        }

        // TODO: Lesson 05 Task 1 - Abbreviate the story

        else
        {
            return story.Substring(0, 20) + "...";
        }// Remove this line.
    }
}//22036043 Yeap Ruo Han
