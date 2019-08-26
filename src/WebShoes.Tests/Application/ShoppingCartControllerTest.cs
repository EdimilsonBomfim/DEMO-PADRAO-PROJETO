using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using WebShoes.API.Controllers;
using WebShoes.API.ViewModel;
using WebShoes.Domain.Entities;
using WebShoes.Domain.Entities.Values;
using WebShoes.Services.Interfaces;
using Xunit;

namespace WebShoes.Tests.Application
{
    public class ShoppingCartControllerTest
    {
        private readonly Mock<IShoppingCartService> _shoppingCartService;
        private readonly Mock<ILogger<ShoppingCartController>> _logger;
        private readonly ShoppingCartController _controller;

        public ShoppingCartControllerTest()
        {
            _shoppingCartService = new Mock<IShoppingCartService>();
            _logger = new Mock<ILogger<ShoppingCartController>>();
            _controller = new ShoppingCartController(_shoppingCartService.Object, _logger.Object);
        }

        [Fact]
        public void PostSuccess()
        {
            _shoppingCartService.Setup(s => s.Add(It.IsAny<long>()));

            var result = _controller.Post(1);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var httpObjResult = result.Result as OkObjectResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 200);

            var value = httpObjResult.Value;

            Assert.NotNull(value);
            Assert.False(string.IsNullOrWhiteSpace(value.ToString()));
            Assert.Same("success", value);
        }

        [Fact]
        public void PostBadRequest()
        {
            _shoppingCartService.Setup(s => s.Add(It.IsAny<long>()));

            _controller.ModelState.AddModelError("Erro", "BadRequest");

            var result = _controller.Post(1);

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
            _shoppingCartService.Setup(s => s.Add(It.IsAny<long>())).Throws(new Exception());

            var result = _controller.Post(1);

            Assert.NotNull(result);
            Assert.IsType<StatusCodeResult>(result.Result);

            var httpObjResult = result.Result as StatusCodeResult;
            Assert.True(httpObjResult.StatusCode == 500);
        }

        [Fact]
        public void GetSuccess()
        {
            _shoppingCartService.Setup(s => s.GetActiveShoppingCart(It.IsAny<long>())).Returns(It.IsAny<ShoppingCart>());
            var result = _controller.Get(1);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var httpObjResult = result.Result as OkObjectResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 200);
        }

        [Fact]
        public void GetInternalError()
        {
            _shoppingCartService.Setup(s => s.GetActiveShoppingCart(It.IsAny<long>())).Throws(new Exception());
            var result = _controller.Get(1);

            Assert.NotNull(result);
            Assert.IsType<StatusCodeResult>(result.Result);

            var httpObjResult = result.Result as StatusCodeResult;
            Assert.True(httpObjResult.StatusCode == 500);
        }

        [Fact]
        public void PatchSuccess()
        {
            _shoppingCartService.Setup(s => s.UpdateStatus(It.IsAny<long>(), It.IsAny<ShoppingCartStatus>()));

            var shoppingCart = new ShoppingCartViewModel();

            var result = _controller.Patch(shoppingCart);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var httpObjResult = result.Result as OkObjectResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 200);

            var value = httpObjResult.Value;

            Assert.NotNull(value);
            Assert.False(string.IsNullOrWhiteSpace(value.ToString()));
            Assert.Same("success", value);
        }

        [Fact]
        public void PatchInternalError()
        {
            _shoppingCartService.Setup(s => s.UpdateStatus(It.IsAny<long>(), It.IsAny<ShoppingCartStatus>())).Throws(new Exception()); ;

            var shoppingCart = new ShoppingCartViewModel();

            var result = _controller.Patch(shoppingCart);

            Assert.NotNull(result);
            Assert.IsType<StatusCodeResult>(result.Result);

            var httpObjResult = result.Result as StatusCodeResult;
            Assert.True(httpObjResult.StatusCode == 500);
        }
    }
}
