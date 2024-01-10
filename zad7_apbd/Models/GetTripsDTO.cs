namespace zad7_apbd.Models;

public class GetTripsDTO
{
    public string Name { get; set; }
    public string Desctiption { get; set; }
    public string DateFrom { get; set; }
    public string DateTo { get; set; }
    public int MaxPeople { get; set; }
    public IEnumerable<CountryDTO> Countries { get; set; }
    public IEnumerable<ClientDTO> Clients { get; set; }
}

public class CountryDTO
{
    public string Name { get; set; }
}

public class ClientDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}