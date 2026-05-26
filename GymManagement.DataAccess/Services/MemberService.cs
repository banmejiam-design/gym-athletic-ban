using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces;

namespace GymManagement.DataAccess.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _repository;

    public MemberService(IMemberRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Member>> GetAllAsync() => _repository.GetMembersWithMembershipsAsync();

    public Task<Member?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

    public async Task<Member> CreateAsync(Member member)
    {
        if (string.IsNullOrWhiteSpace(member.FirstName))
            throw new ArgumentException("First name is required.");
        if (string.IsNullOrWhiteSpace(member.Email))
            throw new ArgumentException("Email is required.");

        var existing = await _repository.GetByEmailAsync(member.Email);
        if (existing is not null)
            throw new InvalidOperationException($"A member with email '{member.Email}' already exists.");

        member.RegistrationDate = DateTime.UtcNow;
        return await _repository.CreateAsync(member);
    }

    public async Task<Member> UpdateAsync(Member member)
    {
        if (!await _repository.ExistsAsync(member.Id))
            throw new KeyNotFoundException($"Member with id {member.Id} not found.");

        var existingByEmail = await _repository.GetByEmailAsync(member.Email);
        if (existingByEmail is not null && existingByEmail.Id != member.Id)
            throw new InvalidOperationException($"Email '{member.Email}' is already in use.");

        return await _repository.UpdateAsync(member);
    }

    public async Task DeleteAsync(int id)
    {
        if (!await _repository.ExistsAsync(id))
            throw new KeyNotFoundException($"Member with id {id} not found.");
        await _repository.DeleteAsync(id);
    }
}
