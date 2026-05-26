using GymManagement.Domain.Entities;

namespace GymManagement.Domain.Interfaces;

public interface ITrainerRepository : IGenericRepository<Trainer>
{
    Task<IEnumerable<Trainer>> GetTrainersWithClassesAsync();
}
