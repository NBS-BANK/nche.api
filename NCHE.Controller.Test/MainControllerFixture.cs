using CommonLibraries.Application.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using VasMicroservices.NCHE.Presentation.Api.Controllers;
using VasMicroservices.NCHE.Infra.Data.Entities;
using VasMicroservices.NCHE.Application.DTOs;
using VasMicroservices.NCHE.Infra.Data.Contexts;
using NCHE.Test.Common;

namespace NCHE.Controller.Test
{
    public class MainControllerFixture
    {
        private readonly Mock<IService<VasPayment, VasPaymentDto, NCHEPaymentsContext>> _paymentServiceMock;
        
        //private readonly ILogger _logger;
        public MainController _controller;
        public MainControllerFixture()
        {
            _paymentServiceMock = new Mock<IService<VasPayment, VasPaymentDto, NCHEPaymentsContext>>();
            _paymentServiceMock.Setup(x => x.CreateAsync(It.IsAny<VasPayment>())).ReturnsAsync(new VasPayment
            {
                 Amount = Convert.ToDecimal(5000.00)
                
            });
            var ncheService = new MockService();
            var log = new Mock<ILogger<MainController>>();
            _controller = new MainController(ncheService,_paymentServiceMock.Object);
        }
    }
}
