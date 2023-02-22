using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VasMicroservices.NCHE.Application.Resources.Requests
{
    public class PaymentRequest
    {
       
            [JsonProperty("invoice_number")]
            public string? InvoiceNumber { get; set; }

            [JsonProperty("amount_paid")]
            public decimal? AmountPaid { get; set; }

            [JsonProperty("reference_number")]
            public string? ReferenceNumber { get; set; }

            [JsonProperty("transaction_date")]
            public string? TransactionDate { get; set; }

            [JsonProperty("description")]
            public string? Description { get; set; }

            [JsonProperty("transaction_source")]
            public string? TransactionSource { get; set; }
        }
    
}
