using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebShoes.Business.Business;
using WebShoes.Business.Interfaces;
using WebShoes.Domain.Entities;
using WebShoes.Domain.Entities.Values;
using WebShoes.Repository.Interfaces;
using Xunit;

namespace WebShoes.Tests.Business
{
    public class SalesOrderBusinessTest
    {
        private SalesOrderBusiness _target;
        private Mock<ISalesOrderRepository> _mockSalesOrderRepository;

        public SalesOrderBusinessTest()
        {
            _mockSalesOrderRepository = new Mock<ISalesOrderRepository>();
            _target = new SalesOrderBusiness(_mockSalesOrderRepository.Object);
        }

        [Fact]
        public void SalesOrderBusinessSuccess()
        {
            var salesOrderExpectancy = new List<SalesOrder>(){ new SalesOrder {
                Id = 1,
                Key = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CustomerId = 1,
                OrderStatus = OrderStatus.WaitingPayment,
                ShoppingCartId = 1,
                ListSalesOrderItem = new List<SalesOrderItem>()
                {
                    new SalesOrderItem() {
                        Id = 1,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        Key = Guid.NewGuid(),
                        ProductId = 1,
                        ProductQuantity = 1,
                        SalesOrderId = 1,
                        UnitPrice = 1000                        
                    }
                }
            } };

            _mockSalesOrderRepository.Setup(m => m.Select(It.IsAny<long>())).Returns(salesOrderExpectancy.FirstOrDefault());
            _mockSalesOrderRepository.Setup(m => m.Add(It.IsAny<SalesOrder>())).Returns(true);
            _mockSalesOrderRepository.Setup(a => a.UpdateStatus(It.IsAny<long>(), It.IsAny<OrderStatus>())).Returns(true);
            _mockSalesOrderRepository.Setup(x => x.Select()).Returns(salesOrderExpectancy);

            foreach (var salesOrder in salesOrderExpectancy)
            {
                var newSalesOrder = _target.Add(salesOrder);
                Assert.True(newSalesOrder);
            }

            var updateStatus = _target.UpdateStatus(1, OrderStatus.PaymentApproved);

            var getSalesOrderUpdate = _target.GetById(1);
            Assert.True(updateStatus);
            Assert.NotNull(getSalesOrderUpdate);
        }
    }
}
