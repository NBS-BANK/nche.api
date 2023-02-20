using Flurl.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VasMicroservices.NRB.Application.Resources.Responses;
using VasMicroservices.NRB.Infrastructure.Data.Entities;
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
        public async Task Test_PostReceipt_ThrowsFlurlException()
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

            fixture._apiService.Setup(x => x.PostAsync<ReceiptResponse>(
                  It.IsAny<string>(),
                  It.IsAny<Dictionary<string, string>>(),
                  It.IsAny<object>(),
                  It.IsAny<Func<string, string, int, Task>>()
               )).Throws(exception);

            await Assert.ThrowsAsync<FlurlHttpException>(async () =>
            {
                var result = await fixture._service.PostReceiptAsync(
                                       new VasMicroservices.NRB.Application.Resources.Requests.ReceiptRequest
                                       {

                                       }
                );

            });
        }
        [Fact]
        public async Task Test_PostReceipt_ThrowsException()
        {
           
            fixture._apiService.Setup(x => x.PostAsync<ReceiptResponse>(
                  It.IsAny<string>(),
                  It.IsAny<Dictionary<string, string>>(),
                  It.IsAny<object>(),
                  It.IsAny<Func<string, string, int, Task>>()
               )).Throws<Exception>();
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                var result = await fixture._service.PostReceiptAsync(
                        new VasMicroservices.NRB.Application.Resources.Requests.ReceiptRequest
                        {

                        }
               );
            });

        }
        [Fact]
        public async Task Test_PostReceipt_Successful()
        {
            var response = new ReceiptResponse
            {
                 CustomerId = "11111"

            };
            fixture._apiService.Setup(x => x.PostAsync<ReceiptResponse>(
                  It.IsAny<string>(),
                  It.IsAny<Dictionary<string, string>>(),
                  It.IsAny<object>(),
                  It.IsAny<Func<string, string, int, Task>>()
               )).Returns(Task.FromResult(
                  (response, 200)
            ));

            var result = await fixture._service.PostReceiptAsync(
                new VasMicroservices.NRB.Application.Resources.Requests.ReceiptRequest
                {

                }
               );
            Assert.NotNull(result);
            Assert.IsType<ReceiptResponse>(result);
            Assert.Equal("11111", result.CustomerId);
            Assert.IsType<string>(result.CustomerId);

        }
        [Fact]
        public async Task Test_GetServices_ThrowsException()
        {
            ICollection<PaymentServices> list = new List<PaymentServices>();
            fixture._paymentServiceMock.Setup(
                x => x.GetByConditionAsync(It.IsAny<Expression<Func<PaymentServices, bool>>>())
                ).Returns(Task.FromResult(list));
            fixture._apiService.Setup(x => x.GetAsync<ICollection<ServicesResponse>>(
                   It.IsAny<string>(),
                   It.IsAny<Dictionary<string, string>>(),
                   It.IsAny<Func<string, string, int, Task>>()
                )).Throws<Exception>();

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                var result = await fixture._service.GetServicesAsync();
            });
            
        }
        [Fact]
        public async Task Test_GetServices_Returns_List()
        {
            ICollection<ServicesResponse> serviceResponses = new List<ServicesResponse>()
            {
                new ServicesResponse
                {

                }
            };
            fixture._apiService.Setup(x => x.GetAsync<ICollection<ServicesResponse>>(
                   It.IsAny<string>(),
                   It.IsAny<Dictionary<string, string>>(),
                   It.IsAny<Func<string, string, int, Task>>()
                )).Returns(Task.FromResult(
                   (serviceResponses, 200)
             ));
            var result = await fixture._service.GetServicesAsync();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
        [Fact]
        public async Task Test_ValidateTransaction_TransactionNotFound_throwsFlurlException()
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
           
            fixture._apiService.Setup(x => x.GetAsync<ICollection<ValidateTransactionResponse>>(
                  It.IsAny<string>(),
                  It.IsAny<Dictionary<string, string>>(),
                  It.IsAny<Func<string, string, int, Task>>()
                  )).Throws(exceptionMock);

            await Assert.ThrowsAsync<FlurlHttpException>(async () =>
            {


                var result = await fixture._service.ValidateTransactionAsync("1235");

            });
        }
        [Fact]
        public async Task Test_ValidateTransaction_TransactionNotFound_throwsException()
        {
            fixture._apiService.Setup(x => x.GetAsync<ICollection<ValidateTransactionResponse>>(
                  It.IsAny<string>(),
                  It.IsAny<Dictionary<string, string>>(),
                  It.IsAny<Func<string, string, int, Task>>()
                  )).Throws<Exception>();

            await Assert.ThrowsAsync<Exception>(async () =>
            {
             

                var result = await fixture._service.ValidateTransactionAsync("1235");

            });
         }
        [Fact]
        public async Task Test_ValidateTransaction_TransactionFound_ReturnsCollection()
        {
            ICollection<ValidateTransactionResponse> responses = new List<ValidateTransactionResponse>()
            {
                new ValidateTransactionResponse
                {
                    TokenId = "1234"
                },
                 new ValidateTransactionResponse
                {
                    TokenId = "12344"
                }

            };
            fixture._apiService.Setup(x => x.GetAsync<ICollection<ValidateTransactionResponse>>(
               It.IsAny<string>(),
               It.IsAny<Dictionary<string, string>>(),
               It.IsAny<Func<string, string, int, Task>>()
               )).Returns(Task.FromResult((responses,200)));

            var result = await fixture._service.ValidateTransactionAsync("1235");
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
       
    }
}
