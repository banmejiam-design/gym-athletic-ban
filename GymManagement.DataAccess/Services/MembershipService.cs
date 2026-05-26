using GymManagement.Domain.Entities;
using GymManagement.Domain.Enums;
using GymManagement.Domain.Interfaces;

namespace GymManagement.DataAccess.Services;

public class MembershipService : IMembershipService
{
    private readonly IMembershipRepository _repository;
    private readonly IMemberRepository _memberRepository;

    public MembershipService(IMembershipRepository repository, IMemberRepository memberRepository)
    {
        _repository = repository;
        _memberRepository = memberRepository;
    }

    public Task<IEnumerable<Membership>> GetAllAsync() => _repository.GetAllAsync();

    public Task<Membership?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

    public Task<IEnumerable<Membership>> GetByMemberIdAsync(int memberId) =>
        _repository.GetByMemberIdAsync(memberId);

    public async Task<Membership> CreateAsync(Membership membership)
    {
        if (!await _memberRepository.ExistsAsync(membership.MemberId))
            throw new KeyNotFoundException($"Member with id {membership.MemberId} not found.");

        if (membership.StartDate >= membership.EndDate)
            throw new ArgumentException("Start date must be before end date.");

        if (membership.Price <= 0)
            throw new ArgumentException("Price must be greater than 0.");

        var existing = await _repository.GetActiveMembershipAsync(membership.MemberId);
        if (existing is not null)
            throw new InvalidOperationException("Member already has an active membership.");

        membership.Status = MembershipStatus.Active;
        return await _repository.CreateAsync(membership);
    }

    public async Task<Membership> UpdateAsync(Membership membership)
    {
        if (!await _repository.ExistsAsync(membership.Id))
            throw new KeyNotFoundException($"Membership with id {membership.Id} not found.");
        if (membership.StartDate >= membership.EndDate)
            throw new ArgumentException("Start date must be before end date.");

        return await _repository.UpdateAsync(membership);
    }

    public async Task DeleteAsync(int id)
    {
        if (!await _repository.ExistsAsync(id))
            throw new KeyNotFoundException($"Membership with id {id} not found.");
        await _repository.DeleteAsync(id);
    }
}
