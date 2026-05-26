using GymManagement.Domain.Entities;

namespace GymManagement.Domain.Interfaces;

public interface IMembershipRepository : IGenericRepository<Membership>
{
    Task<IEnumerable<Membership>> GetByMemberIdAsync(int memberId);
    Task<Membership?> GetActiveMembershipAsync(int memberId);
}
