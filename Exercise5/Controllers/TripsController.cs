using Exercise5.Models.DTOs;
using Exercise5.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exercise5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripsService _tripsService;
        public TripsController(ITripsService tripsService)
        {
            _tripsService = tripsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var output = await _tripsService.GetTripsWithCustomData();
            return Ok(output);
        }

        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> addClientToTrip(ClientPostTrip clientPost, int idTrip)
        {
            if (clientPost == null)
            {
                return BadRequest("Bad information");
            }

            if (!await _tripsService.ClientExists(clientPost.Pesel))
            {
                await _tripsService.addClient(clientPost);
            }

            if (_tripsService.TripExists(clientPost, idTrip))
            {
                if (_tripsService.TripAssingmentCheck(clientPost, idTrip))
                {
                    return Conflict("A Client is already assigned to this trip");
                }
                await _tripsService.addClientToTrip(clientPost, idTrip);
            }
            else
            {
                return NotFound($"TripID: {idTrip} - Does not exist");
            }
            return Ok();
        }
    }
}
