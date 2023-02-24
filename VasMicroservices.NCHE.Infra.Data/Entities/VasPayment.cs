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
        public string? InvoiceNumber { get; set; }
        public string? CustomerName { get; set; }
        public decimal Amount { get; set; }     
        public int? Reversed { get; set; }
        public DateTime? ReversedTime { get; set; }
        public int? NchestatusCode { get; set; }

        public decimal? ApplicationFee { get; set; }

        public decimal? Balance { get; set; }
    }
}
