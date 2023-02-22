using Flurl.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VasMicroservices.NCHE.Application.Resources.Responses;
using Xunit;

namespace NRB.Application.Test
{
    [Collection(nameof(NCHEServiceCollection))]
    public class NCHEServiceTest
    {
        NCHEServiceFixture fixture;

        public NCHEServiceTest(NCHEServiceFixture fixture)
        {
            this.fixture = fixture;
        }
        [Fact]
        public async Task Test_PostTransaction_ThrowsFlurlException()
        {
           
            var call = new FlurlCall();
            var response = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.NoContent

            };
            call.HttpResponseMessage = response;

            var exception = new FlurlHttpException(
                call,
                "Error",
                new Exception("Error")
            );

            fixture._apiService.Setup(x => x.PostAsync<PaymentResponse>(
                  It.IsAny<string>(),
                  It.IsAny<Dictionary<string, string>>(),
                  It.IsAny<object>(),
                  It.IsAny<Func<string, string, int, Task>>()
               )).Throws(exception);

            await Assert.ThrowsAsync<FlurlHttpException>(async () =>
            {
                var result = await fixture._service.PostPaymentAsync(
                    new VasMicroservices.NCHE.Application.Resources.Requests.PaymentRequest
                    {

                    });

            });
        }
        [Fact]
        public async Task Test_PostTransaction_ThrowsException()
        {
           
            fixture._apiService.Setup(x => x.PostAsync<PaymentResponse>(
                  It.IsAny<string>(),
                  It.IsAny<Dictionary<string, string>>(),
                  It.IsAny<object>(),
                  It.IsAny<Func<string, string, int, Task>>()
               )).Throws<Exception>();
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                var result = await fixture._service.PostPaymentAsync(
                   new VasMicroservices.NCHE.Application.Resources.Requests.PaymentRequest
                   {

                   });
            });

        }
        [Fact]
        public async Task Test_PostTransaction_Successful()
        {
            var response = new PaymentResponse
            {
                Transaction = new Transaction
                {
                     InvoiceNumber = "11111"
                }

            };
            fixture._apiService.Setup(x => x.PostAsync<PaymentResponse>(
                  It.IsAny<string>(),
                  It.IsAny<Dictionary<string, string>>(),
                  It.IsAny<object>(),
                  It.IsAny<Func<string, string, int, Task>>()
               )).Returns(Task.FromResult(
                  (response, 200)
            ));

            var result = await fixture._service.PostPaymentAsync(
                  new VasMicroservices.NCHE.Application.Resources.Requests.PaymentRequest
                  {

            });
            Assert.NotNull(result);
            Assert.IsType<PaymentResponse>(result);
            Assert.Equal("11111", result.Transaction.InvoiceNumber);

        }
       
        [Fact]
        public async Task Test_ValidateInvoice_InvoiceNotFound_throwsFlurlException()
        {
            var callMock = new Mock<FlurlCall>();
            var call = new FlurlCall();
            var response = new HttpResponseMessage
            {
                 StatusCode = System.Net.HttpStatusCode.NoContent

            };
            call.HttpResponseMessage = response;
           
            var exceptionMock = new FlurlHttpException(
                call,
                "Error",
                new Exception("Error")
            );
           
            fixture._apiService.Setup(x => x.GetAsync<ValidationResponse>(
                  It.IsAny<string>(),
                  It.IsAny<Dictionary<string, string>>(),
                  It.IsAny<Func<string, string, int, Task>>()
                  )).Throws(exceptionMock);

            await Assert.ThrowsAsync<FlurlHttpException>(async () =>
            {


                var result = await fixture._service.ValidateInvoiceAsync("500");

            });
        }
        [Fact]
        public async Task Test_ValidateInvoice_InvoiceNotFound_throwsException()
        {
            fixture._apiService.Setup(x => x.GetAsync<ValidationResponse>(
                  It.IsAny<string>(),
                  It.IsAny<Dictionary<string, string>>(),
                  It.IsAny<Func<string, string, int, Task>>()
                  )).Throws<Exception>();

            await Assert.ThrowsAsync<Exception>(async () =>
            {
             

                var result = await fixture._service.ValidateInvoiceAsync("404");

            });
         }
        [Fact]
        public async Task Test_ValidateInvoice_InvoiceFound_ReturnsTransaction()
        {
            var response = new ValidationResponse
            {
                 Transaction = new Transaction
                 {
                     InvoiceNumber = "200"
                 }
            };
            fixture._apiService.Setup(x => x.GetAsync<ValidationResponse>(
               It.IsAny<string>(),
               It.IsAny<Dictionary<string, string>>(),
               It.IsAny<Func<string, string, int, Task>>()
               )).Returns(Task.FromResult((response,200)));

            var result = await fixture._service.ValidateInvoiceAsync("200");
            Assert.True(result.InvoiceNumber == "200");
            
        }
       
    }
}
