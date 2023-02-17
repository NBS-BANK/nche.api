using CommonLibraries.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VasMicroservices.NCHE.Application.DTOs
{
    public class VasPaymentDto : DTO
    {
        public int VasPaymentId { get; set; }
        public DateTime TransactionDate { get; set; }

        public VasOfsLogDto? OfsLog { get; set; }
        public string? PaymentReferenceNumber { get; set; }
        public string? Code { get; set; }
        public string? ServiceName { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? PaymentDetails { get; set; }
        public decimal Amount { get; set; }
        public int? Reversed { get; set; }
        public DateTime? ReversedTime { get; set; }
        public string? NcheTxnRef { get; set; }
        public int? NchestatusCode { get; set; }
        public string? RequestString { get; set; }
        public string? ResponseString { get; set; }
    }
}
