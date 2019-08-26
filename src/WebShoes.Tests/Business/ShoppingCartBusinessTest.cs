using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebShoes.Business.Business;
using WebShoes.Business.Interfaces;
using WebShoes.Domain;
using WebShoes.Domain.Entities;
using WebShoes.Domain.Entities.Values;
using WebShoes.Repository.Interfaces;
using Xunit;

namespace WebShoes.Tests.Business
{
    public class ShoppingCartBusinessTest
    {
        private ShoppingCartBusiness _target;
        private Mock<IShoppingCartRepository> _mockShoppingCartRepository;
        private Mock<ICustormeBusiness> _mockCustomerBusiness;


        public ShoppingCartBusinessTest()
        {
            _mockShoppingCartRepository = new Mock<IShoppingCartRepository>();
            _mockCustomerBusiness = new Mock<ICustormeBusiness>();
            _target = new ShoppingCartBusiness(_mockShoppingCartRepository.Object, _mockCustomerBusiness.Object);
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

            _mockShoppingCartRepository.Setup(a => a.GetActiveShoppingCart(It.IsAny<long>())).Returns(shoppingCartExpectancy.FirstOrDefault());

            _mockShoppingCartRepository.Setup(a => a.Insert(It.IsAny<ShoppingCart>())).Returns(true) ;
            _mockShoppingCartRepository.Setup(a => a.UpdateStatus(It.IsAny<long>(),It.IsAny<ShoppingCartStatus>())).Returns(true);
            _mockShoppingCartRepository.Setup(x => x.Select()).Returns(shoppingCartExpectancy);
            _mockShoppingCartRepository.Setup(x => x.Select(It.IsAny<long>())).Returns(shoppingCartExpectancy.FirstOrDefault()); 
            _mockCustomerBusiness.Setup(x => x.Select(It.IsAny<long>())).Returns(new Customer());

            foreach (var shoppingCart in shoppingCartExpectancy)
            {
                var newShoppingCart = _target.Add(shoppingCart);
                Assert.True(newShoppingCart);
            }

            var updateStatus = _target.UpdateStatus(1, ShoppingCartStatus.Closed);

            var getActiveShoppingCart = _target.GetActiveShoppingCart(1);
            Assert.True(updateStatus);
            Assert.NotNull(getActiveShoppingCart);
        }

        [Fact]
        public void ShoppingCartBusinessError()
        {
            var shoppingCartExpectancy = new List<ShoppingCart>(){ new ShoppingCart {
                Id = 1,
                Key = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CartStatus = ShoppingCartStatus.Pending,
                CustomerId = 1
            } };

            _mockShoppingCartRepository.Setup(a => a.GetActiveShoppingCart(It.IsAny<long>())).Returns(shoppingCartExpectancy.FirstOrDefault());

            _mockShoppingCartRepository.Setup(a => a.Insert(It.IsAny<ShoppingCart>())).Returns(true);
            _mockShoppingCartRepository.Setup(a => a.UpdateStatus(It.IsAny<long>(), It.IsAny<ShoppingCartStatus>())).Returns(true);
            _mockShoppingCartRepository.Setup(x => x.Select()).Returns(shoppingCartExpectancy); 
            

            foreach (var shoppingCart in shoppingCartExpectancy)
            {
              Exception exClientNotFound = Assert.Throws<ArgumentException>(() => _target.Add(shoppingCart));

                Assert.Equal($"Cliente não encontrado : {shoppingCart.Id}", exClientNotFound.Message);
            }

            Exception exCartNotFound = Assert.Throws<ArgumentException>(() => _target.UpdateStatus(1, ShoppingCartStatus.Closed));

            Assert.Equal($"Carrinho não encontrado : 1", exCartNotFound.Message);
           
        }
    }
}
