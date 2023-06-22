using Exercise5.Data;
using Exercise5.Models;
using Exercise5.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Exercise5.Services
{
    public interface IClientsService
    {
        public Task RemClient(int id);
        public Task<bool> ClientExists(int id);
        public Task<bool> hasTrips(int id);
    }
    public class ClientsService : IClientsService
    {
        private readonly Apbd5Context _context;
        public ClientsService(Apbd5Context context)
        {
            _context = context;
        }
        public async Task RemClient(int id)
        {
            var client = _context.Clients.Find(id);
            _context.Clients.Remove(client);
            _context.SaveChanges();
        }

        public async Task<bool> ClientExists(int id)
        {
            var output = _context.Clients.Select(e => e.IdClient).Where(e => e.Equals(id)).ToList();
            return output.Any();
        }
        

        public async Task<bool> hasTrips(int id)
        {
            var output = _context.Clients.Include(e => e.ClientTrips).FirstOrDefault(e => e.IdClient == id);
            return output.ClientTrips.Any();
        }
    }
}
