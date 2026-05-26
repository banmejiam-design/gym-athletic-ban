using GymManagement.Domain.Enums;

namespace GymManagement.Domain.Entities;

public class Membership
{
    public int Id { get; set; }
    public MembershipType Type { get; set; }
    public MembershipStatus Status { get; set; } = MembershipStatus.Pending;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }

    public int MemberId { get; set; }
    public Member Member { get; set; } = null!;
}
