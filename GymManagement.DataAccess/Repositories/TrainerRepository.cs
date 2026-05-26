using GymManagement.DataAccess.Context;
using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.DataAccess.Repositories;

public class TrainerRepository : GenericRepository<Trainer>, ITrainerRepository
{
    public TrainerRepository(GymDbContext context) : base(context) { }

    public async Task<IEnumerable<Trainer>> GetTrainersWithClassesAsync() =>
        await _context.Trainers
            .Include(t => t.GymClasses)
            .ToListAsync();
}
