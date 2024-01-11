using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zad7_apbd.Models;
using zad7_apbd.Repo;

namespace zad7_apbd.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ILogger<TripsController> _logger;
    private readonly DatabaseContext _context;

    public TripsController(ILogger<TripsController> logger, DatabaseContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<GetTripsDTO>> getTrips()
    {
        return await _context.Trips
            .OrderByDescending(t => t.DateFrom)
            .Select(t => new GetTripsDTO
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                Countries = t.Countries.Select(country => new CountryDTO
                {
                    Name = country.Name
                }),
                Clients = t.ClientTrips.Select(clientTrip => new ClientDTO
                {
                    FirstName = clientTrip.Client.FirstName,
                    LastName = clientTrip.Client.LastName
                })
            })
            .ToListAsync();
    }
    
    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AddClientToTrip(int idTrip, AddClientToTripDTO addClientToTripDTO)
    {
        var trip = await _context.Trips
            //dadanie Include, aby zrobiło JOIN z tabelą `ClientTrips`
            .Include(t => t.ClientTrips)
            .FirstOrDefaultAsync(t => t.IdTrip == idTrip);
        
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Pesel == addClientToTripDTO.Pesel);
        
        //sprawdzenie czy podana wycieczka istnieje
        if (trip == null)
        {
            return NotFound("Nie znaleziono wycieczki");
        }
        
        // sprawdzenie czy są jeszcze wolne miejsca na tą wycieczkę
        // jeśli są wolne miejsca mogę dodać nowego klienta lub przypisać istniejącego klienta
         if (trip.ClientTrips.Count >= trip.MaxPeople)
         {
             return BadRequest("Wszytkie miejsca na podaną wycieczkę są już zajęte");
         }
        
        //sprawszenie czy klient o podanym numerze PESEL istnieje, jeśli nie dodanie do bazy danych
        if (client == null)
        {
            client = new Client
            {
                FirstName = addClientToTripDTO.FirstName,
                LastName = addClientToTripDTO.LastName,
                Email = addClientToTripDTO.Email,
                Telephone = addClientToTripDTO.Telephone,
                Pesel = addClientToTripDTO.Pesel
            };
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Dodano nowego klienta do bazy danych");
        }
        
        //sprawdzenie czy klient jest już zapisany na daną wycieczkę
        if (trip.ClientTrips.Any(ct => ct.IdClient == client.IdClient))
        {
            return BadRequest("Klient jest już zapisany na tę wycieczkę");
        }

        var clientTrip = new ClientTrip
        {
            IdClient = client.IdClient,
            IdTrip = trip.IdTrip,
            RegisteredAt = DateTime.Now,
            PaymentDate = addClientToTripDTO.PaymentDate
        };
        
        await _context.ClientTrips.AddAsync(clientTrip);
        await _context.SaveChangesAsync();
        return Ok("Dodano klienta do wycieczki");
    }
    
}