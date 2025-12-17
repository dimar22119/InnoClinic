using ProfilesService.Domain.Entities;

namespace ProfilesService.Application.Interfaces.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient?> GetByIdAsync(Guid id);
        Task<IEnumerable<Patient>> GetAllAsync();
        Task AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Patient>> GetByNameAsync(string firstName, string lastName);
    }
}
