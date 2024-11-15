using Microsoft.AspNetCore.Http;

namespace eMuhasebeServer.Domain.Entities.Dtos.KatCizelgeDto;

public class KatCizelgeOlusturmaDto
{
    public int? No { get; set; }
    public string? Tag { get; set; }
    public IFormFile? File { get; set; }
    public string? FilePath { get; set; }
    public int EditCount { get; set; }
    public string HaveTblIlceId { get; init; } = "1";
    public string? CizelgeList { get; set; }
}
