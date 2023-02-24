using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VasMicroservices.NCHE.Application.Resources.Responses
{
    

    public class ValidationResponse
    {
        [JsonProperty("candidate_exists")]
        public bool CandidateExists { get; set; }

        [JsonProperty("transaction")]
        public Transaction Transaction { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }
    }

  

    public class Transaction
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("invoice_number")]
        public string InvoiceNumber { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("amount_paid")]
        public double AmountPaid { get; set; }

        [JsonProperty("application_fee")]
        public string ApplicationFee { get; set; }

        [JsonProperty("balance")]
        public double Balance { get; set; }

        [JsonProperty("reference_number")]
        public string ReferenceNumber { get; set; }
    }
}
