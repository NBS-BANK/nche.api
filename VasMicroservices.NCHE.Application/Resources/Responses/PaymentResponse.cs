using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VasMicroservices.NCHE.Application.Resources.Responses
{

    public class Links
    {
        [JsonProperty("self")]
        public Self Self { get; set; }
    }
    


    public class PaymentResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }

        [JsonProperty("transaction")]
        public Transaction Transaction { get; set; }
    }

    public class Self
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}
