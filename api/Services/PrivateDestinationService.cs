using api.Dtos.PrivateDestination;
using api.Exceptions;
using api.Models;
using api.Repositories;

namespace api.Services
{
    public class PrivateDestinationsService : IPrivateDestinationsService
    {
        private readonly DatabaseContext context;
         private readonly IPermission permission;

        public PrivateDestinationsService(DatabaseContext context, IPermission permission)
        {
            this.context = context;
            this.permission = permission;
        }
        
        public Task<IEnumerable<PrivateDestinationDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PrivateDestinationDto> GetByIdAsync(Guid id)
        {
            var privateDestination = await this.context.PrivateDestinations.FindAsync(id);

            if (privateDestination is null)
            {
                throw new NotFoundException("Destination not found.");
            }

            this.permission.Check(privateDestination.UserId);

            return privateDestination.AsDto();
        }

        public async Task<PrivateDestinationDto> AddAsync(AddPrivateDestinationDto privateDestination)
        {
            this.permission.Check(privateDestination.UserId);

            var newPrivateDestination = new PrivateDestination 
            {
                Id = Guid.NewGuid(),
                Geolocation = privateDestination.Geolocation,
                Title = privateDestination.Title,
                Image = privateDestination.Image,
                Description = privateDestination.Description,
                StartDate = privateDestination.StartDate,
                EndDate = privateDestination.EndDate,
                UserId = privateDestination.UserId
            };

            // TODO: validate new private destination

            await this.context.PrivateDestinations.AddAsync(newPrivateDestination);
            await this.context.SaveChangesAsync();

            return newPrivateDestination.AsDto();
        }

        public async Task UpdateAsync(Guid id, UpdatePrivateDestinationDto privateDestination)
        {
            var oldPrivateDestination = await this.context.PrivateDestinations.FindAsync(id);
            
            if (oldPrivateDestination is null)
            {
                throw new NotFoundException("Destination not found.");
            }

            this.permission.Check(oldPrivateDestination.UserId);
            
            // TODO: validate new private destination

            oldPrivateDestination.Geolocation = privateDestination.Geolocation == string.Empty ?
                oldPrivateDestination.Geolocation : privateDestination.Geolocation;
            oldPrivateDestination.Title = privateDestination.Title == string.Empty ?
                oldPrivateDestination.Title : privateDestination.Title;
            oldPrivateDestination.Image = privateDestination.Image == string.Empty ?
                oldPrivateDestination.Image : privateDestination.Image;
            oldPrivateDestination.Description = privateDestination.Description == string.Empty ?
                oldPrivateDestination.Description : privateDestination.Description;
            oldPrivateDestination.StartDate = privateDestination.StartDate == DateTime.MinValue ?
                oldPrivateDestination.StartDate : privateDestination.StartDate;
            oldPrivateDestination.EndDate = privateDestination.EndDate == DateTime.MinValue ?
                oldPrivateDestination.EndDate : privateDestination.EndDate;

            await this.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var privateDestination = await this.context.PrivateDestinations.FindAsync(id);

            if (privateDestination is null) 
            {
                throw new NotFoundException("Destination not found.");
            }

            this.permission.Check(privateDestination.UserId);

            this.context.Remove(privateDestination);
            await this.context.SaveChangesAsync();
        }

    }
}