namespace GymManagement.API.DTOs;

public record EnrollmentDto(
    int Id,
    int MemberId,
    string MemberName,
    int GymClassId,
    string ClassName,
    DateTime EnrollmentDate,
    bool IsActive
);

public record CreateEnrollmentDto(
    int MemberId,
    int GymClassId
);
