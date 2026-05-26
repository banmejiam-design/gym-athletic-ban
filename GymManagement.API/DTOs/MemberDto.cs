namespace GymManagement.API.DTOs;

public record MemberDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    DateTime DateOfBirth,
    DateTime RegistrationDate
);

public record CreateMemberDto(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    DateTime DateOfBirth
);

public record UpdateMemberDto(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    DateTime DateOfBirth
);
