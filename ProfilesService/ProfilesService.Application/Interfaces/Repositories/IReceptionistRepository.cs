using ProfilesService.Domain.Entities;

namespace ProfilesService.Application.Interfaces.Repositories
{
    public interface IReceptionistRepository
    {
        Task<Receptionist?> GetByIdAsync(Guid id);
        Task<IEnumerable<Receptionist>> GetAllAsync();
        Task AddAsync(Receptionist receptionist);
        Task UpdateAsync(Receptionist receptionist);
        Task DeleteAsync(Guid id);
    }
}
