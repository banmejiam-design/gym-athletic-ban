using GymManagement.DataAccess.Context;
using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.DataAccess.Repositories;

public class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
{
    public EnrollmentRepository(GymDbContext context) : base(context) { }

    public async Task<IEnumerable<Enrollment>> GetByMemberIdAsync(int memberId) =>
        await _context.Enrollments
            .Include(e => e.GymClass)
                .ThenInclude(g => g.Trainer)
            .Where(e => e.MemberId == memberId)
            .ToListAsync();

    public async Task<IEnumerable<Enrollment>> GetByClassIdAsync(int classId) =>
        await _context.Enrollments
            .Include(e => e.Member)
            .Where(e => e.GymClassId == classId)
            .ToListAsync();

    public async Task<bool> IsMemberEnrolledAsync(int memberId, int classId) =>
        await _context.Enrollments.AnyAsync(e => e.MemberId == memberId && e.GymClassId == classId && e.IsActive);
}
