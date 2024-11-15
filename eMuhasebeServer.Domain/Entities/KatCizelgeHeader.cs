using eMuhasebeServer.Domain.Abstractions;

namespace eMuhasebeServer.Domain.Entities;

public class KatCizelgeHeader 
{
    public int Id { get; set; }
    public int No { get; set; }
    public string? Tag { get; set; }
    public string? FilePath { get; set; }
    public int EditCount { get; set; }
    public string? HaveTblIlceId { get; set; }
    public List<KatCizelge>? KatCizelgeler { get; set; }
}
