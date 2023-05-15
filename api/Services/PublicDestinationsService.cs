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

        public async Task UpdateAsync(Guid id, UpdatePublicDestinationDto publicDestination)
        {
            var oldPublicDestination = await this.context.PublicDestinations.FindAsync(id);
            
            if (oldPublicDestination is null)
            {
                throw new NotFoundException("Destination not found.");
            }
            
            // TODO: validate new public destination

            oldPublicDestination.Geolocation = publicDestination.Geolocation == string.Empty ?
                oldPublicDestination.Geolocation : publicDestination.Geolocation;
            oldPublicDestination.Title = publicDestination.Title == string.Empty ?
                oldPublicDestination.Title : publicDestination.Title;
            oldPublicDestination.Image = publicDestination.Image == string.Empty ?
                oldPublicDestination.Image : publicDestination.Image;
            oldPublicDestination.Description = publicDestination.Description == string.Empty ?
                oldPublicDestination.Description : publicDestination.Description;

            await this.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var publicDestination = await this.context.PublicDestinations.FindAsync(id);

            if (publicDestination is null) 
            {
                throw new NotFoundException("Destination not found.");
            }

            this.context.Remove(publicDestination);
            await this.context.SaveChangesAsync();
        }
    }
}