using GymManagement.DataAccess.Context;
using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.DataAccess.Repositories;

public class GymClassRepository : GenericRepository<GymClass>, IGymClassRepository
{
    public GymClassRepository(GymDbContext context) : base(context) { }

    public async Task<IEnumerable<GymClass>> GetClassesWithTrainerAsync() =>
        await _context.GymClasses
            .Include(g => g.Trainer)
            .ToListAsync();

    public async Task<GymClass?> GetClassWithEnrollmentsAsync(int id) =>
        await _context.GymClasses
            .Include(g => g.Trainer)
            .Include(g => g.Enrollments)
                .ThenInclude(e => e.Member)
            .FirstOrDefaultAsync(g => g.Id == id);

    public async Task<int> GetEnrollmentCountAsync(int classId) =>
        await _context.Enrollments.CountAsync(e => e.GymClassId == classId && e.IsActive);
}
