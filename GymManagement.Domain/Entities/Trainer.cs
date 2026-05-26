namespace GymManagement.Domain.Entities;

public class Trainer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public DateTime HireDate { get; set; } = DateTime.UtcNow;

    public ICollection<GymClass> GymClasses { get; set; } = new List<GymClass>();
}
