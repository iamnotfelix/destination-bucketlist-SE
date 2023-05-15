using api.Dtos.PublicDestinations;

namespace api.Services
{
    public interface IPublicDestinationsService
    {
        Task<IEnumerable<PublicDestinationDto>> GetAllAsync();
        Task<PublicDestinationDto> AddAsync(AddPublicDestinationDto publicDestination);
        // Task<T> GetByIdAsync(Guid id);
        // Task UpdateAsync(Guid id, T entity);
        // Task DeleteAsync(Guid id);
    }
}