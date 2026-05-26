using GymManagement.DataAccess.Context;
using GymManagement.Domain.Entities;
using GymManagement.Domain.Enums;
using GymManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.DataAccess.Repositories;

public class MembershipRepository : GenericRepository<Membership>, IMembershipRepository
{
    public MembershipRepository(GymDbContext context) : base(context) { }

    public async Task<IEnumerable<Membership>> GetByMemberIdAsync(int memberId) =>
        await _context.Memberships
            .Include(ms => ms.Member)
            .Where(ms => ms.MemberId == memberId)
            .ToListAsync();

    public async Task<Membership?> GetActiveMembershipAsync(int memberId) =>
        await _context.Memberships
            .Where(ms => ms.MemberId == memberId && ms.Status == MembershipStatus.Active && ms.EndDate >= DateTime.UtcNow)
            .OrderByDescending(ms => ms.EndDate)
            .FirstOrDefaultAsync();
}
