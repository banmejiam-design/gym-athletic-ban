using GymManagement.Domain.Entities;

namespace GymManagement.Domain.Interfaces;

public interface IMemberRepository : IGenericRepository<Member>
{
    Task<Member?> GetByEmailAsync(string email);
    Task<IEnumerable<Member>> GetMembersWithMembershipsAsync();
}
