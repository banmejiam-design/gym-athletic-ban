namespace GymManagement.Domain.Entities;

public class Enrollment
{
    public int Id { get; set; }
    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    public int MemberId { get; set; }
    public Member Member { get; set; } = null!;

    public int GymClassId { get; set; }
    public GymClass GymClass { get; set; } = null!;
}
