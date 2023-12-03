namespace Lesson07.Models;

public class Subscription
{
    public int SubscriptionId { get; set; }
    public int SubscriberId { get; set; }
    public int ProviderId { get; set; }
    public DateTime DateSubscribed { get; set; }
}
