using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces;

namespace GymManagement.DataAccess.Services;

public class GymClassService : IGymClassService
{
    private readonly IGymClassRepository _repository;
    private readonly ITrainerRepository _trainerRepository;

    public GymClassService(IGymClassRepository repository, ITrainerRepository trainerRepository)
    {
        _repository = repository;
        _trainerRepository = trainerRepository;
    }

    public Task<IEnumerable<GymClass>> GetAllAsync() => _repository.GetClassesWithTrainerAsync();

    public Task<GymClass?> GetByIdAsync(int id) => _repository.GetClassWithEnrollmentsAsync(id);

    public async Task<GymClass> CreateAsync(GymClass gymClass)
    {
        if (string.IsNullOrWhiteSpace(gymClass.Name))
            throw new ArgumentException("Class name is required.");
        if (gymClass.MaxCapacity <= 0)
            throw new ArgumentException("Max capacity must be greater than 0.");
        if (gymClass.DurationMinutes <= 0)
            throw new ArgumentException("Duration must be greater than 0.");
        if (!await _trainerRepository.ExistsAsync(gymClass.TrainerId))
            throw new InvalidOperationException($"Trainer with id {gymClass.TrainerId} does not exist.");

        return await _repository.CreateAsync(gymClass);
    }

    public async Task<GymClass> UpdateAsync(GymClass gymClass)
    {
        if (!await _repository.ExistsAsync(gymClass.Id))
            throw new KeyNotFoundException($"GymClass with id {gymClass.Id} not found.");
        if (gymClass.MaxCapacity <= 0)
            throw new ArgumentException("Max capacity must be greater than 0.");

        return await _repository.UpdateAsync(gymClass);
    }

    public async Task DeleteAsync(int id)
    {
        if (!await _repository.ExistsAsync(id))
            throw new KeyNotFoundException($"GymClass with id {id} not found.");
        await _repository.DeleteAsync(id);
    }
}
