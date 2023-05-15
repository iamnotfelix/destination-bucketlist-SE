using api.Dtos.PublicDestinations;

namespace api.Services
{
    public interface IPublicDestinationsService
    {
        Task<IEnumerable<PublicDestinationDto>> GetAllAsync();
        // Task<T> GetByIdAsync(Guid id);
        // Task<T> AddAsync(T entity);
        // Task UpdateAsync(Guid id, T entity);
        // Task DeleteAsync(Guid id);
    }
}