using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces;

namespace GymManagement.DataAccess.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _repository;
    private readonly IMemberRepository _memberRepository;
    private readonly IGymClassRepository _classRepository;

    public EnrollmentService(IEnrollmentRepository repository, IMemberRepository memberRepository, IGymClassRepository classRepository)
    {
        _repository = repository;
        _memberRepository = memberRepository;
        _classRepository = classRepository;
    }

    public Task<IEnumerable<Enrollment>> GetAllAsync() => _repository.GetAllAsync();

    public Task<Enrollment?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

    public Task<IEnumerable<Enrollment>> GetByMemberIdAsync(int memberId) => _repository.GetByMemberIdAsync(memberId);

    public async Task<Enrollment> EnrollAsync(int memberId, int gymClassId)
    {
        if (!await _memberRepository.ExistsAsync(memberId))
            throw new KeyNotFoundException($"Member with id {memberId} not found.");

        var gymClass = await _classRepository.GetClassWithEnrollmentsAsync(gymClassId)
            ?? throw new KeyNotFoundException($"GymClass with id {gymClassId} not found.");

        if (await _repository.IsMemberEnrolledAsync(memberId, gymClassId))
            throw new InvalidOperationException("Member is already enrolled in this class.");

        var currentEnrollments = await _classRepository.GetEnrollmentCountAsync(gymClassId);
        if (currentEnrollments >= gymClass.MaxCapacity)
            throw new InvalidOperationException("This class has reached its maximum capacity.");

        var enrollment = new Enrollment
        {
            MemberId = memberId,
            GymClassId = gymClassId,
            EnrollmentDate = DateTime.UtcNow,
            IsActive = true
        };

        return await _repository.CreateAsync(enrollment);
    }

    public async Task UnenrollAsync(int id)
    {
        var enrollment = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Enrollment with id {id} not found.");

        enrollment.IsActive = false;
        await _repository.UpdateAsync(enrollment);
    }
}
