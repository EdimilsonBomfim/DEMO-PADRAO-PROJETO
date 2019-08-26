using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebShoes.Business.Interfaces;
using WebShoes.Domain.Entities;
using WebShoes.Domain.Entities.Values;
using WebShoes.Services.Services;
using Xunit;

namespace WebShoes.Tests.Service
{
    public class ShoppingCartServiceTest
    {
        private ShoppingCartService _target;
        private Mock<IShoppingCartBusiness> _mockShoppingCartBusiness;

        public ShoppingCartServiceTest()
        {
            _mockShoppingCartBusiness = new Mock<IShoppingCartBusiness>();

            _target = new ShoppingCartService(_mockShoppingCartBusiness.Object);
        }

        [Fact]
        public void ShoppingCartBusinessSuccess()
        {
            var shoppingCartExpectancy = new List<ShoppingCart>(){ new ShoppingCart {
                Id = 1,
                Key = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CartStatus = ShoppingCartStatus.Pending,
                CustomerId = 1
            } };

            _mockShoppingCartBusiness.Setup(a => a.GetActiveShoppingCart(It.IsAny<long>())).Returns(shoppingCartExpectancy.FirstOrDefault());
            _mockShoppingCartBusiness.Setup(a => a.Add(It.IsAny<ShoppingCart>())).Returns(true);
            _mockShoppingCartBusiness.Setup(a => a.UpdateStatus(It.IsAny<long>(), It.IsAny<ShoppingCartStatus>())).Returns(true);

            foreach (var shoppingCart in shoppingCartExpectancy)
            {
                var newShoppingCart = _target.Add(shoppingCart.CustomerId);
                Assert.True(newShoppingCart);
            }

            var getActiveShoppingCart = _target.GetActiveShoppingCart(1);
            var updateStatus = _target.UpdateStatus(1, ShoppingCartStatus.Closed);

            Assert.True(updateStatus);
            Assert.NotNull(getActiveShoppingCart);
        }
    }
}
