using GymManagement.DataAccess.Context;
using GymManagement.Domain.Entities;
using GymManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.DataAccess.Seeders;

public static class DataSeeder
{
    public static async Task SeedAsync(GymDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Trainers.AnyAsync()) return;

        var trainers = new List<Trainer>
        {
            new() { FirstName = "Carlos", LastName = "Rodríguez", Email = "carlos.rodriguez@gym.com", Phone = "310-1234567", Specialization = "CrossFit & HIIT", HireDate = new DateTime(2022, 1, 15) },
            new() { FirstName = "Laura", LastName = "Martínez", Email = "laura.martinez@gym.com", Phone = "311-2345678", Specialization = "Yoga & Pilates", HireDate = new DateTime(2021, 6, 1) },
            new() { FirstName = "Andrés", LastName = "García", Email = "andres.garcia@gym.com", Phone = "312-3456789", Specialization = "Spinning & Cardio", HireDate = new DateTime(2023, 3, 10) }
        };
        await context.Trainers.AddRangeAsync(trainers);
        await context.SaveChangesAsync();

        var classes = new List<GymClass>
        {
            new() { Name = "CrossFit Matutino", Description = "Entrenamiento funcional de alta intensidad", Schedule = DateTime.UtcNow.AddDays(1).Date.AddHours(7), DurationMinutes = 60, MaxCapacity = 15, Status = ClassStatus.Scheduled, TrainerId = trainers[0].Id },
            new() { Name = "Yoga Relajante", Description = "Clase de yoga para reducir el estrés", Schedule = DateTime.UtcNow.AddDays(1).Date.AddHours(9), DurationMinutes = 75, MaxCapacity = 20, Status = ClassStatus.Scheduled, TrainerId = trainers[1].Id },
            new() { Name = "Spinning Avanzado", Description = "Clase de spinning de alto rendimiento", Schedule = DateTime.UtcNow.AddDays(2).Date.AddHours(18), DurationMinutes = 50, MaxCapacity = 12, Status = ClassStatus.Scheduled, TrainerId = trainers[2].Id },
            new() { Name = "HIIT Explosivo", Description = "Intervalos de alta intensidad quema grasa", Schedule = DateTime.UtcNow.AddDays(3).Date.AddHours(6), DurationMinutes = 45, MaxCapacity = 10, Status = ClassStatus.Scheduled, TrainerId = trainers[0].Id },
            new() { Name = "Pilates Core", Description = "Fortalecimiento del núcleo corporal", Schedule = DateTime.UtcNow.AddDays(2).Date.AddHours(10), DurationMinutes = 60, MaxCapacity = 18, Status = ClassStatus.Scheduled, TrainerId = trainers[1].Id }
        };
        await context.GymClasses.AddRangeAsync(classes);
        await context.SaveChangesAsync();

        var members = new List<Member>
        {
            new() { FirstName = "Valentina", LastName = "López", Email = "valentina.lopez@email.com", Phone = "313-4567890", DateOfBirth = new DateTime(1995, 4, 20), RegistrationDate = DateTime.UtcNow.AddMonths(-6) },
            new() { FirstName = "Sebastián", LastName = "Torres", Email = "sebastian.torres@email.com", Phone = "314-5678901", DateOfBirth = new DateTime(1990, 8, 15), RegistrationDate = DateTime.UtcNow.AddMonths(-3) },
            new() { FirstName = "Camila", LastName = "Hernández", Email = "camila.hernandez@email.com", Phone = "315-6789012", DateOfBirth = new DateTime(1998, 12, 5), RegistrationDate = DateTime.UtcNow.AddMonths(-1) },
            new() { FirstName = "Juan", LastName = "Vargas", Email = "juan.vargas@email.com", Phone = "316-7890123", DateOfBirth = new DateTime(1988, 3, 25), RegistrationDate = DateTime.UtcNow.AddDays(-15) },
            new() { FirstName = "María", LastName = "Díaz", Email = "maria.diaz@email.com", Phone = "317-8901234", DateOfBirth = new DateTime(2000, 7, 10), RegistrationDate = DateTime.UtcNow.AddDays(-5) }
        };
        await context.Members.AddRangeAsync(members);
        await context.SaveChangesAsync();

        var memberships = new List<Membership>
        {
            new() { MemberId = members[0].Id, Type = MembershipType.Annual,    Status = MembershipStatus.Active, StartDate = DateTime.UtcNow.AddMonths(-6), EndDate = DateTime.UtcNow.AddMonths(6),  Price = 600000 },
            new() { MemberId = members[1].Id, Type = MembershipType.Quarterly, Status = MembershipStatus.Active, StartDate = DateTime.UtcNow.AddMonths(-2), EndDate = DateTime.UtcNow.AddMonths(1),  Price = 200000 },
            new() { MemberId = members[2].Id, Type = MembershipType.Monthly,   Status = MembershipStatus.Active, StartDate = DateTime.UtcNow.AddDays(-10),  EndDate = DateTime.UtcNow.AddDays(20),  Price = 80000  },
            new() { MemberId = members[3].Id, Type = MembershipType.Monthly,   Status = MembershipStatus.Active, StartDate = DateTime.UtcNow.AddDays(-5),   EndDate = DateTime.UtcNow.AddDays(25),  Price = 80000  },
            new() { MemberId = members[4].Id, Type = MembershipType.Quarterly, Status = MembershipStatus.Pending, StartDate = DateTime.UtcNow,              EndDate = DateTime.UtcNow.AddMonths(3), Price = 200000 }
        };
        await context.Memberships.AddRangeAsync(memberships);
        await context.SaveChangesAsync();

        var enrollments = new List<Enrollment>
        {
            new() { MemberId = members[0].Id, GymClassId = classes[0].Id, EnrollmentDate = DateTime.UtcNow.AddDays(-5), IsActive = true },
            new() { MemberId = members[0].Id, GymClassId = classes[1].Id, EnrollmentDate = DateTime.UtcNow.AddDays(-4), IsActive = true },
            new() { MemberId = members[1].Id, GymClassId = classes[2].Id, EnrollmentDate = DateTime.UtcNow.AddDays(-3), IsActive = true },
            new() { MemberId = members[1].Id, GymClassId = classes[0].Id, EnrollmentDate = DateTime.UtcNow.AddDays(-2), IsActive = true },
            new() { MemberId = members[2].Id, GymClassId = classes[4].Id, EnrollmentDate = DateTime.UtcNow.AddDays(-1), IsActive = true },
            new() { MemberId = members[3].Id, GymClassId = classes[3].Id, EnrollmentDate = DateTime.UtcNow,             IsActive = true }
        };
        await context.Enrollments.AddRangeAsync(enrollments);
        await context.SaveChangesAsync();
    }
}
