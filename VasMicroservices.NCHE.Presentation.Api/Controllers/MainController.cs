using CommonLibraries.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VasMicroservices.NCHE.Application.Constants;
using VasMicroservices.NCHE.Application.DTOs;
using VasMicroservices.NCHE.Application.Services;
using VasMicroservices.NCHE.Infra.Data.Contexts;
using VasMicroservices.NCHE.Infra.Data.Entities;
using VasMicroservices.NCHE.Presentation.Api.Models;

namespace VasMicroservices.NCHE.Presentation.Api.Controllers
{
    [Route("api/nche")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INCHEService _ncheService;
        private readonly IService<VasPayment, VasPaymentDto, NCHEPaymentsContext> _paymentLogService;

        public MainController(INCHEService ncheService, 
            IService<VasPayment, VasPaymentDto, NCHEPaymentsContext> paymentLogService)
        {
            _ncheService = ncheService;
            _paymentLogService = paymentLogService;
        }

        [HttpGet("searchInvoice/{invoiceNumber}")]
        public async Task<IActionResult> SearchInvoice(string invoiceNumber)
        {
            var result = await _ncheService.ValidateInvoiceAsync(invoiceNumber);
            if(result.InvoiceNumber == null)
            {
                return NotFound($"Invoice number {invoiceNumber}");
            }
            return Ok(new { exists = result.InvoiceNumber != null, transaction = result });
        }
        [HttpPost("postReceipt")]
        public async Task<IActionResult> PostTransaction([FromBody] PostTransactionRequest request)
        {
            var result = await _ncheService.PostPaymentAsync(request.PaymentRequest);
            if (result.Status == Codes.INVOICE_NOT_EXIST)
            {
                return NotFound($"Invoice number {request.PaymentRequest.InvoiceNumber}");
            }
            var logResult = await _paymentLogService.CreateAsync(new VasPayment
            {
               Amount = Convert.ToDecimal(result.Transaction.AmountPaid),
               CustomerName = $"{ result.Transaction.FirstName} {result.Transaction.LastName}",
               InvoiceNumber = result.Transaction.InvoiceNumber,
               NchestatusCode = result.Status,
               PaymentReferenceNumber = result.Transaction.ReferenceNumber,
               OfsLog = new VasOfsLog
               {
                  DebitAccountNumber = request.VasLog?.DebitAccountNumber,
                  OfscompanyCode = request.VasLog?.OfscompanyCode,
                  Ofsmessage = request.VasLog?.Ofsmessage,
                  Ofsrequest = request.VasLog?.Ofsrequest,
                  Ofsresponse = request.VasLog?.Ofsresponse,
                  Ofssuccess = request.VasLog?.Ofssuccess,
                  TellerId = request.VasLog?.TellerId,
                  TellerName = request.VasLog?.TellerName,
                  TxnRef = request.VasLog?.TxnRef                 

               },
               Balance = Convert.ToDecimal(result.Transaction.Balance),
               ApplicationFee = Convert.ToDecimal(result.Transaction.ApplicationFee),
               TransactionDate = DateTime.Now

              


            });
            return Ok(new { posted = result.Status == Codes.SUCCESSFUL, logged = logResult != null });
        }
    }
}
