using Exercise5.Data;
using Exercise5.Models;
using Exercise5.Models.DTOs;

namespace Exercise5.Services
{
    public interface ITripsService
    {
        Task<IEnumerable<TripWithCustomData>> GetTripsWithCustomData();
        Task addClient(ClientPostTrip clientPost);
        Task<bool> ClientExists(string pesel);
        bool TripExists(ClientPostTrip clientPost, int idTrip);
        bool TripAssingmentCheck(ClientPostTrip clientPost, int idTrip);
        Task addClientToTrip(ClientPostTrip clientPost, int tripID);
    }
    public class TripsService : ITripsService
    {
        private readonly Apbd5Context _context;
        public TripsService(Apbd5Context context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TripWithCustomData>> GetTripsWithCustomData()
        {
            var trips = _context.Trips.Select(e => new TripWithCustomData
            {
                Name = e.Name,
                Description = e.Description,
                DateFrom = e.DateFrom,
                DateTo = e.DateTo,
                MaxPeople = e.MaxPeople,
                Countries = e.ColCountries.Select(c => new CountryName { Name = c.Name }),
                Clients = e.ClientTrips.Select(t => new ClientFirstLast
                {
                    FirstName = t.IdClientNavigation.FirstName,
                    LastName = t.IdClientNavigation.LastName,
                })
            }).OrderByDescending(e => e.DateFrom);
            return trips;
        }
        public async Task<bool> ClientExists(string pesel)
        {
            var output = _context.Clients.Select(e => e.Pesel).Where(e => e.Equals(pesel)).ToList();
            return output.Any();
        }

        public async Task addClient(ClientPostTrip clientPost)
        {
            var id = _context.Clients.Max(e => e.IdClient) + 1;
            var client = new Models.Client
            {
                IdClient = id,
                FirstName = clientPost.FirstName,
                LastName = clientPost.LastName,
                Email = clientPost.Email,
                Telephone = clientPost.Telephone,
                Pesel = clientPost.Pesel
            };
            _context.Clients.Add(client);
            _context.SaveChanges();
        }
        public bool TripExists(ClientPostTrip clientPost, int idTrip)
        {
            return _context.Trips.Any(e => e.IdTrip == idTrip);
        }

        public bool TripAssingmentCheck(ClientPostTrip clientPost, int idTrip)
        {
            var client = _context.Clients.FirstOrDefault(e => e.Pesel.Equals(clientPost.Pesel));
            return _context.ClientTrips.Any(e => e.IdClient == client.IdClient && e.IdTrip == idTrip);
        }
        public async Task addClientToTrip(ClientPostTrip clientPost, int tripID)
        {
            var client = _context.Clients.FirstOrDefault(e => e.Pesel.Equals(clientPost.Pesel));
            var clientTrip = new ClientTrip
            {
                IdClient = client.IdClient,
                IdTrip = tripID,
                RegisteredAt = DateTime.Now,
                PaymentDate = clientPost.PaymentDate
            };
            _context.ClientTrips.Add(clientTrip);
            _context.SaveChanges();
        }
    }
}
