using eMuhasebeServer.Domain.Entities.VmModel.SearchVariant;
using eMuhasebeServer.Domain.Entities.VmModel.MbToPartial;
using eMuhasebeServer.Domain.Utilities.MbToPartial;
using eMuhasebeServer.Domain.Utilities.DymSearch;
using eMuhasebeServer.Domain.Entities.Enum;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace eMuhasebeServer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PureMethodsController : ControllerBase
    {
        [Route("MbToPartial")]
        [HttpGet]
        public MbToPartialViewDto MbToPartial()
        {
            return MbToPartialProperty.Get();
        }

        [Route("DymSearch")]
        [HttpGet]
        public SearchFilterVmModel DymSearch(string? search, int? resultMatchNumber)
        {


            //return SearchFilterProperty.Search(new List<SearchVariantDataDto>(), EntityTableEnum.Projeler, search, resultMatchNumber);
            return SearchFilterProperty.Search(new List<SearchVariantDataDto>(), EntityTableEnum.Projeler, search ?? string.Empty, resultMatchNumber);

        }
    }
}
