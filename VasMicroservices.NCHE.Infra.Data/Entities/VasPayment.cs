using CommonLibraries.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VasMicroservices.NCHE.Infra.Data.Entities
{
    public partial class VasPayment : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VasPaymentId { get; set; }
        public DateTime TransactionDate { get; set; }

        public VasOfsLog? OfsLog { get; set; }  
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
