using GymManagement.Domain.Enums;

namespace GymManagement.API.DTOs;

public class GymClassDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Schedule { get; set; }
    public int DurationMinutes { get; set; }
    public int MaxCapacity { get; set; }
    public ClassStatus Status { get; set; }
    public int TrainerId { get; set; }
    public string TrainerName { get; set; } = string.Empty;
}

public class CreateGymClassDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Schedule { get; set; }
    public int DurationMinutes { get; set; }
    public int MaxCapacity { get; set; }
    public int TrainerId { get; set; }
}

public class UpdateGymClassDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Schedule { get; set; }
    public int DurationMinutes { get; set; }
    public int MaxCapacity { get; set; }
    public ClassStatus Status { get; set; }
    public int TrainerId { get; set; }
}
