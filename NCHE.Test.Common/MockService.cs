using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VasMicroservices.NCHE.Application.Constants;
using VasMicroservices.NCHE.Application.Resources.Requests;
using VasMicroservices.NCHE.Application.Resources.Responses;
using VasMicroservices.NCHE.Application.Services;

namespace NCHE.Test.Common
{
    public class MockService : INCHEService
    {
        public Task<PaymentResponse> PostPaymentAsync(PaymentRequest request)
        {
            if (request.InvoiceNumber != null)
            {
                if (request.InvoiceNumber.Equals("47000234600"))
                {
                    return Task.FromResult(new PaymentResponse
                    {

                    });
                }
                else if (request.InvoiceNumber.Equals("4700023460077"))
                {
                    return Task.FromResult(new PaymentResponse
                    {
                           Status = Codes.INVOICE_NOT_EXIST
                    });
                }
            }
            throw new Exception();
        }

        public Task<Transaction> ValidateInvoiceAsync(string invoiceNumber)
        {
            if (invoiceNumber.Equals("47000234600"))
            {
                return Task.FromResult(new Transaction
                {
                    InvoiceNumber = invoiceNumber
                });
            }
            else if (invoiceNumber.Equals("470002346007"))
            {

            }
            else if (invoiceNumber.Equals("4700023460077"))
            {
                return Task.FromResult(new Transaction
                {
                    InvoiceNumber = null
                });
            }
            throw new Exception();
        }
    }
}
