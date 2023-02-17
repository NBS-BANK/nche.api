using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VasMicroservices.NCHE.Application.Resources.Requests;
using VasMicroservices.NCHE.Application.Resources.Responses;
using VasMicroservices.NCHE.Application.Services;

namespace NCHE.Test.Common
{
    public class MockService : INCHEService
    {
        public Task<PaymentResponse> PostPaymentAsync(PaymentRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateInvoiceAsync(string invoiceNumber)
        {
            throw new NotImplementedException();
        }
    }
}
