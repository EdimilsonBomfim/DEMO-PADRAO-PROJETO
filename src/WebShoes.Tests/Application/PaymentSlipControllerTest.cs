using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using WebShoes.API.Controllers;
using WebShoes.API.ViewModel;
using WebShoes.Domain.Entities;
using WebShoes.Services.Interfaces;
using Xunit;

namespace WebShoes.Tests.Application
{
    public class PaymentSlipControllerTest
    {
        private readonly Mock<IPaymentSlipService> _paymentSlipService;
        private readonly Mock<ILogger<PaymentSlipController>> _logger;
        private readonly PaymentSlipController _controller;
        private readonly PaymentSlipViewModel _paymentSlipViewModel;

        public PaymentSlipControllerTest()
        {
            _paymentSlipService = new Mock<IPaymentSlipService>();
            _logger = new Mock<ILogger<PaymentSlipController>>();
            _controller = new PaymentSlipController(_paymentSlipService.Object, _logger.Object);
        }

        [Fact]
        public void PostSuccess()
        {
            _paymentSlipService.Setup(s => s.Insert(It.IsAny<long>(), It.IsAny<string>()));

            var result = _controller.Post(It.IsAny<long>(), It.IsAny<PaymentSlipViewModel>());

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var httpObjResult = result.Result as OkObjectResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 200);

            var value = httpObjResult.Value;

            Assert.NotNull(value);
            Assert.False(string.IsNullOrWhiteSpace(value.ToString()));
            Assert.Same("Success", value);
        }

        [Fact]
        public void PostBadRequest()
        {
            _paymentSlipService.Setup(s => s.Insert(It.IsAny<long>(), It.IsAny<string>()));

            _controller.ModelState.AddModelError("Erro", "BadRequest");

            var result = _controller.Post(It.IsAny<long>(), It.IsAny<PaymentSlipViewModel>());

            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);

            var httpObjResult = result.Result as BadRequestObjectResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 400);

            var value = httpObjResult.Value;

            Assert.NotNull(value);
            Assert.False(string.IsNullOrWhiteSpace(value.ToString()));
        }


        [Fact]
        public void PostInternalError()
        {
            _paymentSlipService.Setup(s => s.Insert(It.IsAny<long>(), It.IsAny<string>())).Throws(new Exception());

            var result = _controller.Post(It.IsAny<long>(), It.IsAny<PaymentSlipViewModel>());

            Assert.NotNull(result);
            Assert.IsType<StatusCodeResult>(result.Result);

            var httpObjResult = result.Result as StatusCodeResult;
            Assert.True(httpObjResult.StatusCode == 500);
        }
    }
}
