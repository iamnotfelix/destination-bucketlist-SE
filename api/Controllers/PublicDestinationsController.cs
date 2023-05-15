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
        
        // GET /publicdestinations/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PublicDestinationDto>>> GetPublicDestinationAsync(Guid id)
        {
            try
            {
                var publicDestination = await this.service.GetByIdAsync(id);
                return Ok(publicDestination);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
        // ADD /publicdestinations
        [HttpPost]
        public async Task<ActionResult<PublicDestinationDto>> AddPublicDestinationAsync(AddPublicDestinationDto publicDestination)
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
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePublicDestinationAsync(Guid id, UpdatePublicDestinationDto publicDestination)
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

        // DELETE /publicdestination/:id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePublicDestiantion(Guid id)
        {
            try
            {
                await this.service.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}