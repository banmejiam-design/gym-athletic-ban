using GymManagement.Domain.Enums;

namespace GymManagement.API.DTOs;

public record GymClassDto(
    int Id,
    string Name,
    string Description,
    DateTime Schedule,
    int DurationMinutes,
    int MaxCapacity,
    ClassStatus Status,
    int TrainerId,
    string TrainerName
);

public record CreateGymClassDto(
    string Name,
    string Description,
    DateTime Schedule,
    int DurationMinutes,
    int MaxCapacity,
    int TrainerId
);

public record UpdateGymClassDto(
    string Name,
    string Description,
    DateTime Schedule,
    int DurationMinutes,
    int MaxCapacity,
    ClassStatus Status,
    int TrainerId
);
