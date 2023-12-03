namespace Lesson07.Models;

public class Subscriber
{
    public int SubscriberId { get; set; }
    public string UserName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string FamilyName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public bool PublicProfile { get; set; }
    public bool AutoAcceptFriends { get; set; }
    public bool BroadcastPosts { get; set; }
}

