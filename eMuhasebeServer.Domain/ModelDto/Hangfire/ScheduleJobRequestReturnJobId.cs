using eMuhasebeServer.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMuhasebeServer.Domain.ModelDto.Hangfire
{
    public sealed class ScheduleJobRequestReturnJobId 
    {
        public string? JobId { get; set; }
        public DateTime StartDate { get; set; }
        public int Interval { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; } 


    }
}
