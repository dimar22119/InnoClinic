using ProfilesService.Domain.Entities;

namespace ProfilesService.Application.Interfaces.Repositories
{
    public interface IDoctorRepository
    {
        Task<Doctor?> GetByIdAsync(Guid id);
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task AddAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Doctor>> GetByNameAsync(string firstName, string lastName);
    }
}
