using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.API.Controllers;
using WebShoes.API.ViewModel;
using WebShoes.Domain.Entities;
using WebShoes.Service.Interfaces;
using Xunit;

namespace WebShoes.Tests.Application
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductService> _productService;
        private readonly Mock<ILogger<ProductController>> _logger;
        private readonly ProductController _controller;

        public ProductControllerTest()
        {
            _productService = new Mock<IProductService>();
            _logger = new Mock<ILogger<ProductController>>();
            _controller = new ProductController(_productService.Object, _logger.Object);
        }

        [Fact]
        public void PostSuccess()
        {
            _productService.Setup(s => s.Add(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(),It.IsAny<int>()));
            ProductViewModel productviewmodel = new ProductViewModel();
            productviewmodel.Code = "1";
            productviewmodel.Description = "Bamba";
            productviewmodel.Quantity = 10;
            productviewmodel.UnitPrice = 125000;

            var result = _controller.Post(productviewmodel);

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
            _productService.Setup(s => s.Add(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));
            ProductViewModel productviewmodel = new ProductViewModel();
            productviewmodel.Code = "1";
            productviewmodel.Description = "Bamba";
            productviewmodel.Quantity = 10;
            productviewmodel.UnitPrice = 125000;

            _controller.ModelState.AddModelError("Erro", "BadRequest");

            var result = _controller.Post(productviewmodel);

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
            _productService.Setup(s => s.Add(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception()); ;
            ProductViewModel productviewmodel = new ProductViewModel();
            productviewmodel.Code = "1";
            productviewmodel.Description = "Bamba";
            productviewmodel.Quantity = 10;
            productviewmodel.UnitPrice = 125000;

            var result = _controller.Post(productviewmodel);

            Assert.NotNull(result);
            Assert.IsType<StatusCodeResult>(result.Result);

            var httpObjResult = result.Result as StatusCodeResult;
            Assert.True(httpObjResult.StatusCode == 500);
        }

        [Fact]
        public void GetSuccessById()
        {
            _productService.Setup(s => s.GetById(It.IsAny<long>())).Returns(It.IsAny<Product>());
            var result = _controller.GetById(1);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var httpObjResult = result.Result as OkObjectResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 200);
        }

        [Fact]
        public void GetInternalErrorById()
        {
            _productService.Setup(s => s.GetById(It.IsAny<long>())).Throws(new Exception());
            var result = _controller.GetById(1);

            Assert.NotNull(result);
            Assert.IsType<StatusCodeResult>(result.Result);

            var httpObjResult = result.Result as StatusCodeResult;
            Assert.True(httpObjResult.StatusCode == 500);
        }


        [Fact]
        public void GetSuccessGetDescription()
        {
            _productService.Setup(s => s.GetDescription(It.IsAny<long>())).Returns(It.IsAny<string>());
            var result = _controller.GetDescripton(1);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var httpObjResult = result.Result as OkObjectResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 200);
        }

        [Fact]
        public void GetInternalErrorGetDescription()
        {
            _productService.Setup(s => s.GetDescription(It.IsAny<long>())).Throws(new Exception());
            var result = _controller.GetDescripton(1);

            Assert.NotNull(result);
            Assert.IsType<StatusCodeResult>(result.Result);

            var httpObjResult = result.Result as StatusCodeResult;
            Assert.True(httpObjResult.StatusCode == 500);
        }


        [Fact]
        public void GetSuccessGetStock()
        {
            _productService.Setup(s => s.GetStock(It.IsAny<long>())).Returns(It.IsAny<int>());
            var result = _controller.GetStock(1);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var httpObjResult = result.Result as OkObjectResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 200);
        }

        [Fact]
        public void GetInternalErrorGetStock()
        {
            _productService.Setup(s => s.GetStock(It.IsAny<long>())).Throws(new Exception());
            var result = _controller.GetStock(1);

            Assert.NotNull(result);
            Assert.IsType<StatusCodeResult>(result.Result);

            var httpObjResult = result.Result as StatusCodeResult;
            Assert.True(httpObjResult.StatusCode == 500);
        }

        [Fact]
        public void GetSuccessGetPrice()
        {
            _productService.Setup(s => s.GetUnitPrice(It.IsAny<long>())).Returns(It.IsAny<int>());
            var result = _controller.GetPrice(1);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var httpObjResult = result.Result as OkObjectResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 200);
        }
        [Fact]
        public void GetInternalErrorGetPrice()
        {
            _productService.Setup(s => s.GetUnitPrice(It.IsAny<long>())).Throws(new Exception());
            var result = _controller.GetPrice(1);

            Assert.NotNull(result);
            Assert.IsType<StatusCodeResult>(result.Result);

            var httpObjResult = result.Result as StatusCodeResult;
            Assert.True(httpObjResult.StatusCode == 500);
        }


        [Fact]
        public void GetSuccessChangeUnitPrice()
        {
            _productService.Setup(s => s.ChangeUnitPrice(It.IsAny<long>(), It.IsAny<int>())).Returns(It.IsAny<bool>());
            var result = _controller.PatchChangeUnitPrice(1, 123400);
            

            Assert.True(result);
        }

        [Fact]
        public void GetInternalErrorChangeUnitPrice()
        {
            _productService.Setup(s => s.ChangeUnitPrice(It.IsAny<long>(), It.IsAny<int>())).Throws(new Exception());
            var result = _controller.PatchChangeUnitPrice(1,123400);

            Assert.False(result);
        }


        [Fact]
        public void GetSuccessIncrease()
        {
            _productService.Setup(s => s.IncreaseQuantity(It.IsAny<long>(), It.IsAny<int>())).Returns(It.IsAny<bool>());
            var result = _controller.PatchIncrease(1, 10);


            Assert.True(result);
        }

        [Fact]
        public void GetInternalErrorIncrease()
        {
            _productService.Setup(s => s.IncreaseQuantity(It.IsAny<long>(), It.IsAny<int>())).Throws(new Exception());
            var result = _controller.PatchIncrease(1, 123400);

            Assert.False(result);
        }



        [Fact]
        public void GetSuccessDecrease()
        {
            _productService.Setup(s => s.DecreaseQuantity(It.IsAny<long>(), It.IsAny<int>())).Returns(It.IsAny<bool>());
            var result = _controller.PatchDecrease(1, 10);


            Assert.True(result);
        }

        [Fact]
        public void GetInternalErrorDecrease()
        {
            _productService.Setup(s => s.DecreaseQuantity(It.IsAny<long>(), It.IsAny<int>())).Throws(new Exception());
            var result = _controller.PatchDecrease(1, 10);

            Assert.False(result);
        }

        [Fact]
        public void GetInternalErrorDecreaseAll()
        {
            _productService.Setup(s => s.DecreaseQuantity(It.IsAny<long>(), It.IsAny<int>()));
            var result = _controller.PatchDecrease(1, 1000000);

            Assert.True(result);
        }

    }
}
