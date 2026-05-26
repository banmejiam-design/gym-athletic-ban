using GymManagement.Domain.Entities;

namespace GymManagement.Domain.Interfaces;

public interface IGymClassService
{
    Task<IEnumerable<GymClass>> GetAllAsync();
    Task<GymClass?> GetByIdAsync(int id);
    Task<GymClass> CreateAsync(GymClass gymClass);
    Task<GymClass> UpdateAsync(GymClass gymClass);
    Task DeleteAsync(int id);
}
