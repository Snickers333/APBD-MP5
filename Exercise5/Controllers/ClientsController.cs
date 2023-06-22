using Exercise5.Models.DTOs;
using Exercise5.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exercise5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService _clientsService;
        public ClientsController(IClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveClient(int id)
        {
            if (await _clientsService.ClientExists(id))
            {
                if (await _clientsService.hasTrips(id))
                {
                    return BadRequest($"Client with ID: {id} - Still has trips assigned");
                }
                else
                {
                    await _clientsService.RemClient(id);
                    return Ok($"Client ID: {id} - Has been deleted successfully");
                }
            }
            else
            {
                return BadRequest($"Client with ID: {id} - Does not exist in the database");
            }

        }
    }
}
