using VasMicroservices.NCHE.Application.Resources.Requests;

namespace VasMicroservices.NCHE.Presentation.Api.Models
{
    public class PostTransactionRequest
    {
        public VasOfsLogRequest? VasLog { get; set; }
        public PaymentRequest PaymentRequest { get; set; }
    }
}
