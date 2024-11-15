namespace eMuhasebeServer.Domain.Entities.Dtos.KatCizelgeDto;

public class KatCizelgeListDto
{
    public int CizelgeId { get; set; }
    public string? HaveTblIlce { get; set; }
    public List<KatCizelgeDto>? Data { get; set; }
}
