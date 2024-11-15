using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMuhasebeServer.Domain.Entities.VmModel.MbToPartial
{
    public class MbToPartialReturnListDto
    {
        public int Id { get; set; }

        public int? FirstPartition { get; set; }
        public int? SecondPartition { get; set; }
        public int? ThirdPartition { get; set; }

        public string Dosyaboyutu { get; set; } = string.Empty;
    }
}
