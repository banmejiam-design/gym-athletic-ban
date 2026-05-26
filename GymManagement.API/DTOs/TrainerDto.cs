namespace GymManagement.API.DTOs;

public record TrainerDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string Specialization,
    DateTime HireDate
);

public record CreateTrainerDto(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string Specialization
);

public record UpdateTrainerDto(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string Specialization
);
