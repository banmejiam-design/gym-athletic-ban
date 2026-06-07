namespace GymManagement.API.DTOs;

public class EnrollmentDto
{
    public int Id { get; set; }
    public int MemberId { get; set; }
    public string MemberName { get; set; } = string.Empty;
    public int GymClassId { get; set; }
    public string ClassName { get; set; } = string.Empty;
    public DateTime EnrollmentDate { get; set; }
    public bool IsActive { get; set; }
}

public class CreateEnrollmentDto
{
    public int MemberId { get; set; }
    public int GymClassId { get; set; }
}
