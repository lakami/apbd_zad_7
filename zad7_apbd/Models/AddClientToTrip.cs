using System.ComponentModel.DataAnnotations;

namespace zad7_apbd.Models;

public class AddClientToTripDTO
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Telephone { get; set; }
    [Required]
    public string Pesel { get; set; }
    [Required]
    public int IdTrip { get; set; }
    [Required]
    public string TripName { get; set; }
    public DateTime? PaymentDate { get; set; }
}


