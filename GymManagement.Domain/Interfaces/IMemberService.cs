using GymManagement.Domain.Entities;

namespace GymManagement.Domain.Interfaces;

public interface IMemberService
{
    Task<IEnumerable<Member>> GetAllAsync();
    Task<Member?> GetByIdAsync(int id);
    Task<Member> CreateAsync(Member member);
    Task<Member> UpdateAsync(Member member);
    Task DeleteAsync(int id);
}
