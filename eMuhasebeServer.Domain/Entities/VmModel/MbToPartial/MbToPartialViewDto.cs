using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMuhasebeServer.Domain.Entities.VmModel.MbToPartial
{
    public class MbToPartialViewDto
    {
        public string? FirstPartition { get; set; }
        public string? SecondPartition { get; set; }
        public string? ThirdPartition { get; set; }

        public List<MbToPartialReturnListDto>? Data { get; set; }
    }
}
