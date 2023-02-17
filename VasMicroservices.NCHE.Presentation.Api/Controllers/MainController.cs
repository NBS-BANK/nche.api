using CommonLibraries.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> SearchTransaction(string invoiceNumber)
        {
            var result = await _ncheService.ValidateInvoiceAsync(invoiceNumber);
            return Ok(new { result });
        }
        [HttpPost("postReceipt")]
        public async Task<IActionResult> PostTransaction([FromBody] PostTransactionRequest request)
        {
            var result = await _ncheService.PostPaymentAsync(request.PaymentRequest);
            return Ok(new { result });
        }
    }
}
