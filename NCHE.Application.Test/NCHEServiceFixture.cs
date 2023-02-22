
using CommonLibraries.Application.Services;
using Moq;
using NCHE.Test.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VasMicroservices.NCHE.Application.DTOs;
using VasMicroservices.NCHE.Application.Services;
using VasMicroservices.NCHE.Application.Settings;
using VasMicroservices.NCHE.Infra.Data.Contexts;
using VasMicroservices.NCHE.Infra.Data.Entities;

namespace NRB.Application.Test
{
    public class NCHEServiceFixture
    {
        public INCHEService _service;
        public readonly Mock<INCHESettings> _settings;
        public readonly Mock<ApiMockRequest> _requestService;
        public readonly Mock<IApiRequestService> _apiService;
        private readonly Mock<IService<EndpointLog, EndpointLogDto, NCHEPaymentsContext>> _logServiceMock;
        //public readonly Mock<IService<VasPayment, VasPaymentDto, NCHEPaymentsContext>> _paymentServiceMock;
        public NCHEServiceFixture()
        {
            _settings = new Mock<INCHESettings>();
            _settings.Setup(x => x.ClientId).Returns("xxxxxxxxxxx");
            _settings.Setup(x => x.AuthToken).Returns("xxxxxxx");
            _settings.Setup(x => x.Url).Returns("https://10.100.101.24:8243/");
            _requestService = new Mock<ApiMockRequest>();

            ICollection<EndpointLog> logs = new List<EndpointLog>()
            {
                new EndpointLog
                {

                },
                new EndpointLog
                {

                }

            };

            _logServiceMock = new Mock<IService<EndpointLog, EndpointLogDto, NCHEPaymentsContext>>();
            _logServiceMock.Setup(x => x.UpdateAsync(It.IsAny<EndpointLog>())).Returns(Task.FromResult(1));
            _logServiceMock.Setup(x => x.UpdateManyAsync(It.IsAny<ICollection<EndpointLog>>())).Returns(Task.FromResult(2));
            _logServiceMock.Setup(x => x.CreateManyAsync(It.IsAny<ICollection<EndpointLog>>())).Returns(Task.FromResult(logs));
            _logServiceMock.Setup(x => x.CreateAsync(It.IsAny<EndpointLog>())).Returns(Task.FromResult(logs.First()));
            _logServiceMock.Setup(x => x.DeleteAsync(It.IsAny<EndpointLog>())).Returns(Task.FromResult(1));
            _logServiceMock.Setup(x => x.DeleteManyAsync(It.IsAny<Expression<Func<EndpointLog, bool>>>())).Returns(Task.FromResult(2));
            _logServiceMock.Setup(x => x.GetAsync()).Returns(Task.FromResult(logs));
            _logServiceMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<EndpointLog, bool>>>())).Returns(Task.FromResult(logs));
            _logServiceMock.Setup(x => x.GetPaginatedAsync(1, 2, null, null, true)).Returns(Task.FromResult(logs));
            _logServiceMock.Setup(x => x.GetPaginatedByConditionAsync(It.IsAny<Expression<Func<EndpointLog, bool>>>(), 1, 2, null, null, true)).Returns(Task.FromResult(logs));

            ICollection <VasPayment> services = new List<VasPayment>()
            {
                    new VasPayment
                    {

                    },
                    new VasPayment
                    {

                    }
            };
            /*_paymentServiceMock = new Mock<IService<VasPayment, VasPaymentDto, NCHEPaymentsContext>>();
            _paymentServiceMock.Setup(x => x.UpdateAsync(new VasPayment { })).Returns(Task.FromResult(1));
            _paymentServiceMock.Setup(x => x.UpdateManyAsync(services)).Returns(Task.FromResult(2));
            _paymentServiceMock.Setup(x => x.CreateManyAsync(services)).Returns(Task.FromResult(services));
            _paymentServiceMock.Setup(x => x.CreateAsync(services.First())).Returns(Task.FromResult(services.First()));
            _paymentServiceMock.Setup(x => x.DeleteAsync(services.First())).Returns(Task.FromResult(1));
            _paymentServiceMock.Setup(x => x.DeleteManyAsync(It.IsAny<Expression<Func<VasPayment, bool>>>())).Returns(Task.FromResult(2));
            _paymentServiceMock.Setup(x => x.GetAsync()).Returns(Task.FromResult(services));
            _paymentServiceMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<VasPayment, bool>>>())).Returns(Task.FromResult(services));
            _paymentServiceMock.Setup(x => x.GetPaginatedAsync(1, 2, null, null, true)).Returns(Task.FromResult(services));
            _paymentServiceMock.Setup(x => x.GetPaginatedByConditionAsync(It.IsAny<Expression<Func<VasPayment, bool>>>(), 1, 2, null, null, true)).Returns(Task.FromResult(services));
             */
             
            _apiService = new Mock<IApiRequestService>();
          
           

            _service = new NCHEService(_settings.Object,_apiService.Object, _logServiceMock.Object);
        }
    }
}
