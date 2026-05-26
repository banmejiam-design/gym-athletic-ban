using GymManagement.Domain.Entities;

namespace GymManagement.Domain.Interfaces;

public interface IGymClassRepository : IGenericRepository<GymClass>
{
    Task<IEnumerable<GymClass>> GetClassesWithTrainerAsync();
    Task<GymClass?> GetClassWithEnrollmentsAsync(int id);
    Task<int> GetEnrollmentCountAsync(int classId);
}
