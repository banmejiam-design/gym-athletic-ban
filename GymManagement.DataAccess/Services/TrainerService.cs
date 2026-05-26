using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces;

namespace GymManagement.DataAccess.Services;

public class TrainerService : ITrainerService
{
    private readonly ITrainerRepository _repository;

    public TrainerService(ITrainerRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Trainer>> GetAllAsync() => _repository.GetTrainersWithClassesAsync();

    public Task<Trainer?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

    public async Task<Trainer> CreateAsync(Trainer trainer)
    {
        if (string.IsNullOrWhiteSpace(trainer.FirstName))
            throw new ArgumentException("First name is required.");
        if (string.IsNullOrWhiteSpace(trainer.Email))
            throw new ArgumentException("Email is required.");
        if (string.IsNullOrWhiteSpace(trainer.Specialization))
            throw new ArgumentException("Specialization is required.");

        trainer.HireDate = DateTime.UtcNow;
        return await _repository.CreateAsync(trainer);
    }

    public async Task<Trainer> UpdateAsync(Trainer trainer)
    {
        if (!await _repository.ExistsAsync(trainer.Id))
            throw new KeyNotFoundException($"Trainer with id {trainer.Id} not found.");
        return await _repository.UpdateAsync(trainer);
    }

    public async Task DeleteAsync(int id)
    {
        if (!await _repository.ExistsAsync(id))
            throw new KeyNotFoundException($"Trainer with id {id} not found.");
        await _repository.DeleteAsync(id);
    }
}
