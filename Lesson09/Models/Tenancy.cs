using System.ComponentModel.DataAnnotations;

namespace Lesson09.Models;

public class Tenancy
{

    public string TenantId { get; set; } = null!;


    public string TenantEmail { get; set; } = null!;


    public string CreditCard { get; set; } = null!;


    public int? RoomId { get; set; } = null;


    public float? WeeklyRental { get; set; } = null;


    public DateTime? StartDate { get; set; } = null;
}
