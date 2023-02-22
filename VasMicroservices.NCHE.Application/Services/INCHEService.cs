using VasMicroservices.NCHE.Application.Resources.Requests;
using VasMicroservices.NCHE.Application.Resources.Responses;

namespace VasMicroservices.NCHE.Application.Services
{
    public interface INCHEService
    {
        Task<PaymentResponse> PostPaymentAsync(PaymentRequest request);
        Task<Transaction> ValidateInvoiceAsync(string invoiceNumber);
    }
}