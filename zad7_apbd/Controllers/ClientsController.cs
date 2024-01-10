using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zad7_apbd.Repo;

namespace zad7_apbd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly ILogger<TripsController> _logger;
    private readonly DatabaseContext _context;


    public ClientsController(ILogger<TripsController> logger, DatabaseContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    [HttpDelete("{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        var client = await _context.Clients
                //dadanie Include, aby zrobiło JOIN z tabelą `ClientTrips`
            .Include(c => c.ClientTrips)
            .FirstOrDefaultAsync(c => c.IdClient == idClient);
        
        if (client == null)
        {
            return NotFound("Nie znaleziono klienta");
        }
        
        if (client.ClientTrips.Count > 0)
        {
            return BadRequest("Nie można usunąć klienta, który ma przypisane wycieczki");
        } 

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return Ok("Usunięto klienta");
    }
}