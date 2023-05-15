using api.Dtos.PublicDestinations;
using Microsoft.EntityFrameworkCore;
using api;
using api.Exceptions;
using api.Repositories;
using api.Models;

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

        public async Task<PublicDestinationDto> AddAsync(AddPublicDestinationDto publicDestination)
        {
            var newPublicDestination = new Destination 
            {
                Id = Guid.NewGuid(),
                Geolocation = publicDestination.Geolocation,
                Title = publicDestination.Title,
                Image = publicDestination.Image,
                Description = publicDestination.Description
            };

            // TODO: validate new public destination

            await this.context.PublicDestinations.AddAsync(newPublicDestination);
            await this.context.SaveChangesAsync();

            return newPublicDestination.AsDto();
        }
    }
}