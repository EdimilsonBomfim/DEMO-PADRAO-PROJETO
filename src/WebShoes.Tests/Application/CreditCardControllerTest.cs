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
    public class CreditCardControllerTest
    {
        private readonly Mock<ICreditCardService> _creditCardService;
        private readonly Mock<ILogger<CreditCardController>> _logger;
        private readonly CreditCardController _controller;
        private readonly CreditCardViewModel _creditCardViewModel;

        public CreditCardControllerTest()
        {
            _creditCardService = new Mock<ICreditCardService>();
            _logger = new Mock<ILogger<CreditCardController>>();
            _controller = new CreditCardController(_creditCardService.Object, _logger.Object);
        }

        [Fact]
        public void PostSuccess()
        {
            _creditCardService.Setup(s => s.Insert(It.IsAny<long>(), It.IsAny<string>()));

            var result = _controller.Post(It.IsAny<long>(), It.IsAny<CreditCardViewModel>());

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
            _creditCardService.Setup(s => s.Insert(It.IsAny<long>(), It.IsAny<string>())).Throws(new Exception());;

            _controller.ModelState.AddModelError("Erro", "BadRequest");

            var result = _controller.Post(It.IsAny<long>(), It.IsAny<CreditCardViewModel>());

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
            _creditCardService.Setup(s => s.Insert(It.IsAny<long>(), It.IsAny<string>())).Throws(new Exception());

            var result = _controller.Post(It.IsAny<long>(), It.IsAny<CreditCardViewModel>());

            Assert.NotNull(result);
            Assert.IsType<StatusCodeResult>(result.Result);

            var httpObjResult = result.Result as StatusCodeResult;
            Assert.True(httpObjResult.StatusCode == 500);
        }
    }
}
