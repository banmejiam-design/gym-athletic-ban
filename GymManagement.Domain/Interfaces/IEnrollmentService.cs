using GymManagement.Domain.Entities;

namespace GymManagement.Domain.Interfaces;

public interface IEnrollmentService
{
    Task<IEnumerable<Enrollment>> GetAllAsync();
    Task<Enrollment?> GetByIdAsync(int id);
    Task<Enrollment> EnrollAsync(int memberId, int gymClassId);
    Task UnenrollAsync(int id);
    Task<IEnumerable<Enrollment>> GetByMemberIdAsync(int memberId);
}
