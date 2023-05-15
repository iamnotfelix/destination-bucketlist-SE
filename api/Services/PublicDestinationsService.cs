using api.Dtos.PublicDestinations;
using Microsoft.EntityFrameworkCore;
using api;
using api.Exceptions;
using api.Repositories;

namespace api.Services
{
    public class PublicDestinationsService : IPublicDestinationsService
    {
        private readonly DatabaseContext context;

        public PublicDestinationsService(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<PublicDestinationDto>> GetAllAsync()
        {
            var publicDestinations = await this.context.PublicDestinations.ToListAsync();

            if (publicDestinations is null)
            {
                throw new NotFoundException("Destinations not found.");
            }

            return publicDestinations.Select(destination => destination.AsDto());
        }
    }
}