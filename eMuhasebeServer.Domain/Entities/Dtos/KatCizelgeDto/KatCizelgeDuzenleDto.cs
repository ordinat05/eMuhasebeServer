namespace eMuhasebeServer.Domain.Entities.Dtos.KatCizelgeDto;

public class KatCizelgeDuzenleDto
{
    public int Id { get; set; }
    public string? TagText { get; set; }
    public string? CizelgeIcerik { get; set; }
    public int[] HaveLocationId { get; init; } = new int[] { 1, 2 };
}
