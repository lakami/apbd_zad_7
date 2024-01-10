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
}