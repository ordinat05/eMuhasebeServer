using eMuhasebeServer.Domain.Abstractions;

namespace eMuhasebeServer.Domain.Entities;


public class KatCizelge 
{
    public int Id { get; set; }
    public string? KatAdi { get; set; }
    public string? DaireAdi { get; set; }
    public int KatCizelgeHeaderId { get; set; }
    public KatCizelgeHeader? KatCizelgeHeader { get; set; }
}
