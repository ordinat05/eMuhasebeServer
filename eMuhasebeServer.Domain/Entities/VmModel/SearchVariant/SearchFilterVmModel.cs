using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMuhasebeServer.Domain.Entities.VmModel.SearchVariant;

public class SearchFilterVmModel
{
    public int? ArananCumleKelimeSayisiSplit { get; set; }
    public List<SearchVariantDataDto> SearchVariantDataDto { get; set; } = null!;
    //public List<FiltrelenenSonuclarDto> FiltrelenenSonuclarDto { get; set; } = new List<FiltrelenenSonuclarDto>();
    //public List<FiltrelenenSonuclarDto> FiltrelenenSonuclarDto { get; set; } = new List<FiltrelenenSonuclarDto>();
    //public List<FiltrelenenSonuclarDto> FiltrelenenSonuclarDto { get; set; } = new List<FiltrelenenSonuclarDto>();
    public List<FiltrelenenSonuclarDto> FiltrelenenSonuclarDto { get; set; } = null!;
}
