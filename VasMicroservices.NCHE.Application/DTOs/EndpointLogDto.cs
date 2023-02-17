using CommonLibraries.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VasMicroservices.NCHE.Application.DTOs
{
    public class EndpointLogDto : DTO
    {
        public int RecordId { get; set; }
        public string? RequestUrl { get; set; }
        public string? RawRequest { get; set; }
        public string? RawResponse { get; set; }
        public int? Code { get; set; }
        public int? Success { get; set; }
        public string? Message { get; set; }
        public DateTime? RequestTime { get; set; }
        public DateTime? ResponseTime { get; set; }
    }
}
