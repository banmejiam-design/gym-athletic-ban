using GymManagement.Domain.Enums;

namespace GymManagement.Domain.Entities;

public class GymClass
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Schedule { get; set; }
    public int DurationMinutes { get; set; }
    public int MaxCapacity { get; set; }
    public ClassStatus Status { get; set; } = ClassStatus.Scheduled;

    public int TrainerId { get; set; }
    public Trainer Trainer { get; set; } = null!;

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
