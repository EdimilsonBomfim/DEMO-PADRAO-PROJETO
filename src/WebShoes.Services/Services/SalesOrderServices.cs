using System;
using System.Collections.Generic;
using System.Linq;
using WebShoes.Business.Interfaces;
using WebShoes.Domain.Entities;
using WebShoes.Domain.Entities.Values;
using WebShoes.Services.Interfaces;

namespace WebShoes.Services.Services
{
    public class SalesOrderServices : ISalesOrderServices
    {
        private readonly IShoppingCartBusiness _shoppingCartBusiness;
        private readonly IShoppingCartItemBusiness _shoppingCartItemBusiness;
        private readonly ISalesOrderBusiness _salesOrderBusiness;
        private readonly IProductBusiness _productBusiness;

        public SalesOrderServices(IShoppingCartBusiness shoppingCartBusiness, IShoppingCartItemBusiness shoppingCartItemBusiness, ISalesOrderBusiness salesOrderBusiness, IProductBusiness productBusiness)
        {
            _shoppingCartItemBusiness = shoppingCartItemBusiness;
            _shoppingCartBusiness = shoppingCartBusiness;
            _salesOrderBusiness = salesOrderBusiness;
            _productBusiness = productBusiness;
        }

        public IEnumerable<SalesOrder> GetByDate(DateTime date)
        {
            return _salesOrderBusiness.GetByDate(date);
        }

        public bool Add(long customerId)
        {
            var shoppingCart = _shoppingCartBusiness.GetActiveShoppingCart(customerId);

            if (shoppingCart == null)
                throw new ArgumentException($"Carrinho não encontrado para o cliente: {customerId}.");

            var listShoppingCartItem = _shoppingCartItemBusiness.GetShoppingCartItems(shoppingCart.Id, true);
            
            if (!listShoppingCartItem.Any())
                throw new ArgumentException($"Carrinho de compras {shoppingCart.Id} está vazio.");

            //var salesOder = _salesOrderBusiness.GetById()

            //Order
            SalesOrder salesOrder = new SalesOrder()
            {
                CustomerId = shoppingCart.CustomerId,
                Id = shoppingCart.Id,
                OrderStatus = OrderStatus.WaitingPayment,
                UpdatedDate = DateTime.Now,
                ShoppingCartId = shoppingCart.Id
            };            

            //OrderItem
            foreach (var shoppingCartItem in listShoppingCartItem)
            {                
                var validateQuantity = _productBusiness.GetStock((int)shoppingCartItem.ProductId);
                var price = _productBusiness.GetUnitPrice((int)shoppingCartItem.ProductId);

                if (validateQuantity < shoppingCartItem.ProductQuantity)
                {
                    throw new ArgumentException($"Saldo insuficiente para o produto: {shoppingCartItem.ProductId}");
                }

                SalesOrderItem salesOrderItem = new SalesOrderItem()
                {
                    SalesOrderId = salesOrder.Id,
                    ProductId = shoppingCartItem.ProductId,
                    ProductQuantity = shoppingCartItem.ProductQuantity,
                    UnitPrice = price
                };

                salesOrder.ListSalesOrderItem.Add(salesOrderItem);
            }            

            return _salesOrderBusiness.Add(salesOrder);
        }        

        public bool UpdateStatus(long salesOrderId, OrderStatus orderStatus)
        {
            return _salesOrderBusiness.UpdateStatus(salesOrderId, orderStatus);
        }  
        
        public SalesOrder GetById(long id)
        {
            return _salesOrderBusiness.GetById(id);
        }

        public SalesOrder GetByShoppingCartId(long shoppingCartId)
        {
            return _salesOrderBusiness.GetByShoppingCartId(shoppingCartId);
        }
    }
}
