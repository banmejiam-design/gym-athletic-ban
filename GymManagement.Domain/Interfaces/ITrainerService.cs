using GymManagement.Domain.Entities;

namespace GymManagement.Domain.Interfaces;

public interface ITrainerService
{
    Task<IEnumerable<Trainer>> GetAllAsync();
    Task<Trainer?> GetByIdAsync(int id);
    Task<Trainer> CreateAsync(Trainer trainer);
    Task<Trainer> UpdateAsync(Trainer trainer);
    Task DeleteAsync(int id);
}
