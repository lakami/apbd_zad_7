using Microsoft.AspNetCore.Mvc;
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
        return new List<GetTripsDTO>();
    }
}