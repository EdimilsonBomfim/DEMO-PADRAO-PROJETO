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
    public class ShoppingCartItemControllerTest
    {
        private readonly Mock<IShoppingCartItemService> _shoppingCartItemService;
        private readonly Mock<ILogger<ShoppingCartItemController>> _logger;
        private readonly ShoppingCartItemController _controller;

        public ShoppingCartItemControllerTest()
        {
            _shoppingCartItemService = new Mock<IShoppingCartItemService>();
            _logger = new Mock<ILogger<ShoppingCartItemController>>();
            _controller = new ShoppingCartItemController(_shoppingCartItemService.Object, _logger.Object);
        }

        [Fact]
        public void PostSuccess()
        {
            _shoppingCartItemService.Setup(s => s.Add(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>()));

            var cartItem = new ShoppingCartItemViewModel() { ProductId = 1, ProductQuantity = 1, ShoppingCartId = 1 };


            var result = _controller.Post(cartItem);

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
            _shoppingCartItemService.Setup(s => s.Add(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>()));

            _controller.ModelState.AddModelError("Erro", "BadRequest");

            var result = _controller.Post(It.IsAny<ShoppingCartItemViewModel>());

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
            _shoppingCartItemService.Setup(s => s.Add(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>())).Throws(new Exception());

            var shoppingCartItem = new ShoppingCartItemViewModel();

            var result = _controller.Post(shoppingCartItem);

            Assert.NotNull(result);
            Assert.IsType<StatusCodeResult>(result.Result);

            var httpObjResult = result.Result as StatusCodeResult;
            Assert.True(httpObjResult.StatusCode == 500);
        }

        [Fact]
        public void GetSuccess()
        {
            _shoppingCartItemService.Setup(s => s.GetShoppingCartItem(It.IsAny<long>(), true)).Returns(It.IsAny<List<ShoppingCartItem>>());

            var result = _controller.Get(1, true);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var httpObjResult = result.Result as OkObjectResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 200);
        }

        [Fact]
        public void GetInternalError()
        {
            _shoppingCartItemService.Setup(s => s.GetShoppingCartItem(It.IsAny<long>(), true)).Throws(new Exception());

            var result = _controller.Get(1, true);
            
            Assert.NotNull(result);
            Assert.IsType<StatusCodeResult>(result.Result);

            var httpObjResult = result.Result as StatusCodeResult;
            Assert.True(httpObjResult.StatusCode == 500);
        }
    }
}
