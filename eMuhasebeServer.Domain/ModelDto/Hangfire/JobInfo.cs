using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMuhasebeServer.Domain.ModelDto.Hangfire
{
    public sealed class JobInfo
    {
        public string? Id { get; set; }
        public string? Type { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? NextExecution { get; set; }
    }
}
