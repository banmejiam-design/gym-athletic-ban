using GymManagement.Domain.Enums;

namespace GymManagement.API.DTOs;

public record MembershipDto(
    int Id,
    int MemberId,
    string MemberName,
    MembershipType Type,
    MembershipStatus Status,
    DateTime StartDate,
    DateTime EndDate,
    decimal Price
);

public record CreateMembershipDto(
    int MemberId,
    MembershipType Type,
    DateTime StartDate,
    DateTime EndDate,
    decimal Price
);

public record UpdateMembershipDto(
    MembershipType Type,
    MembershipStatus Status,
    DateTime StartDate,
    DateTime EndDate,
    decimal Price
);
