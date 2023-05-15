using api.Dtos.PublicDestinations;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using api.Exceptions;

namespace api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PublicDestinationsController : ControllerBase
    {
        public PublicDestinationsService service;

        public PublicDestinationsController(IPublicDestinationsService service)
        {
            this.service = (PublicDestinationsService) service;
        }

        // GET /publicdestinations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicDestinationDto>>> GetPublicDestinationsAsync()
        {
            try
            {
                var publicDestinations = await this.service.GetAllAsync();
                return Ok(publicDestinations);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
        // ADD /publicdestinations
        [HttpPost]
        public async Task<ActionResult<PublicDestinationDto>> AddPublicDestinationsAsync(AddPublicDestinationDto publicDestination)
        {
            try
            {
                var newPublicDestinations = await this.service.AddAsync(publicDestination);
                return Ok(newPublicDestinations);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
        // ADD /publicdestinations/:id
        [HttpPost("id")]
        public async Task<ActionResult> UpdatePublicDestinationsAsync(Guid id, UpdatePublicDestinationDto publicDestination)
        {
            try
            {
                await this.service.UpdateAsync(id, publicDestination);
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}