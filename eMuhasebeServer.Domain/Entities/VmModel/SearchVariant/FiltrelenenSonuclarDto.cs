namespace eMuhasebeServer.Domain.Entities.VmModel.SearchVariant;

public class FiltrelenenSonuclarDto
{
    public int Id { get; set; }
    public string? AranacakKelime { get; set; } = string.Empty;
    public int? AranacakKelimeArrayId { get; set; }
    public int? FiltrelenenArrayId { get; set; } = null;
}
