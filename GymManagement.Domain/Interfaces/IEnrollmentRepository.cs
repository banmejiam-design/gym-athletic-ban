using GymManagement.Domain.Entities;

namespace GymManagement.Domain.Interfaces;

public interface IEnrollmentRepository : IGenericRepository<Enrollment>
{
    Task<IEnumerable<Enrollment>> GetByMemberIdAsync(int memberId);
    Task<IEnumerable<Enrollment>> GetByClassIdAsync(int classId);
    Task<bool> IsMemberEnrolledAsync(int memberId, int classId);
}
