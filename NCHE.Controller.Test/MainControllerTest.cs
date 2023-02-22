using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VasMicroservices.NCHE.Application.Resources.Requests;
using VasMicroservices.NCHE.Presentation.Api.Models;
using Xunit;

namespace NCHE.Controller.Test
{
    [Collection(nameof(MainControllerCollection))]
    public class MainControllerTest
    {
        public MainControllerFixture fixture;

        public MainControllerTest(MainControllerFixture fixture)
        {
            this.fixture = fixture;
        }
        [Fact]
        public async Task Test_PostTransaction_ReturnsOk()
        {
            
            var result = await fixture._controller.PostTransaction(new PostTransactionRequest
            {
                   PaymentRequest = new PaymentRequest
                   {
                          InvoiceNumber = "47000234600"
                   }
            });
            
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task Test_PostTransaction_Returns404()
        {
            var result = await fixture._controller.PostTransaction(new PostTransactionRequest
            {
                PaymentRequest = new PaymentRequest
                {
                    InvoiceNumber = "4700023460077"
                }
            });
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async Task Test_SearchInvoice_ReturnsOk()
        {
            var result = await fixture._controller.SearchInvoice("47000234600");
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        /*[Fact]
        public async Task Test_ValidateInvoice_ReturnsBadRequest()
        {
            var result = await fixture._controller.SearchInvoice("470002346007");
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }*/
        [Fact]
        public async Task Test_ValidateInvoice_ReturnsNotFoundResponse()
        {
            var result = await fixture._controller.SearchInvoice("4700023460077");
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        
    }
}
