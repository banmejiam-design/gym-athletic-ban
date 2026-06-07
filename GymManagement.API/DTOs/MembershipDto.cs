using GymManagement.Domain.Enums;

namespace GymManagement.API.DTOs;

public class MembershipDto
{
    public int Id { get; set; }
    public int MemberId { get; set; }
    public string MemberName { get; set; } = string.Empty;
    public MembershipType Type { get; set; }
    public MembershipStatus Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
}

public class CreateMembershipDto
{
    public int MemberId { get; set; }
    public MembershipType Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
}

public class UpdateMembershipDto
{
    public MembershipType Type { get; set; }
    public MembershipStatus Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
}
