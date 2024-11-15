using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMuhasebeServer.Domain.ModelDto.Hangfire
{
    public sealed class JobIdResponse
    {
        public string? ScheduledJobId { get; set; }
        public string? ContinuationJobId { get; set; }
        public string? RecurringJobId { get; set; }
    }
}


//Id    GorevTuru 