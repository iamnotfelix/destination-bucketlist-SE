using api.Dtos.PublicDestinations;
using api.Models;

namespace api
{
    public static class Extension
    {
        public static PublicDestinationDto AsDto(this Destination destination)
        {
            return new PublicDestinationDto
            {
                Id = destination.Id,
                Geolocation = destination.Geolocation,
                Title = destination.Title,
                Image = destination.Image,
                Description = destination.Description
            };
        }
    }
}