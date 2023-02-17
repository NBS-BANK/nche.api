using CommonLibraries.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VasMicroservices.NCHE.Application.DTOs
{
    public class VasOfsLogDto : DTO
    {
        public int RecordId { get; set; }

        public VasPaymentDto? VasPayment { get; set; }
        public int VasPaymentId { get; set; }
        public string? OfscompanyCode { get; set; }
        public string? TellerId { get; set; }
        public string? TellerName { get; set; }
        public string? DebitAccountNumber { get; set; }
        public string? TxnRef { get; set; }
        public byte? Ofssuccess { get; set; }
        public string? Ofsrequest { get; set; }
        public string? Ofsresponse { get; set; }
        public string? Ofsmessage { get; set; }
    }
}
