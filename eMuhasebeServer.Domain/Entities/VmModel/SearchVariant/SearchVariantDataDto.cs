using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMuhasebeServer.Domain.Entities.VmModel.SearchVariant;

public class SearchVariantDataDto
{
    public int Id { get; set; }

    public string? AranacakKelimeler { get; set; } = string.Empty;

    public string? Eslesme { get; set; } = null;

    public int? MatchNumber { get; set; } = 0;

    public List<int>? aranacakKelimeArrayId { get; set; } = new List<int>();
}
