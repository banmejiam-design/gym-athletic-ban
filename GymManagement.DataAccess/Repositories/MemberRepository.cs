using GymManagement.DataAccess.Context;
using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.DataAccess.Repositories;

public class MemberRepository : GenericRepository<Member>, IMemberRepository
{
    public MemberRepository(GymDbContext context) : base(context) { }

    public async Task<Member?> GetByEmailAsync(string email) =>
        await _context.Members.FirstOrDefaultAsync(m => m.Email == email);

    public async Task<IEnumerable<Member>> GetMembersWithMembershipsAsync() =>
        await _context.Members
            .Include(m => m.Memberships)
            .ToListAsync();
}
