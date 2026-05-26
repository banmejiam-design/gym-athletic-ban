using GymManagement.Domain.Entities;

namespace GymManagement.Domain.Interfaces;

public interface IMembershipService
{
    Task<IEnumerable<Membership>> GetAllAsync();
    Task<Membership?> GetByIdAsync(int id);
    Task<Membership> CreateAsync(Membership membership);
    Task<Membership> UpdateAsync(Membership membership);
    Task DeleteAsync(int id);
    Task<IEnumerable<Membership>> GetByMemberIdAsync(int memberId);
}
