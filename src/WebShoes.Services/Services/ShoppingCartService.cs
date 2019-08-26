using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Business.Interfaces;
using WebShoes.Domain.Entities;
using WebShoes.Domain.Entities.Values;
using WebShoes.Services.Interfaces;

namespace WebShoes.Services.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartBusiness _shoppingCartBusiness;

        public ShoppingCartService(IShoppingCartBusiness shoppingCartBusiness)
        {
            _shoppingCartBusiness = shoppingCartBusiness;
        }

        public bool Add(long customerId)
        {
            var shoppingCart = new ShoppingCart();
            shoppingCart.CartStatus = ShoppingCartStatus.Pending;
            shoppingCart.CustomerId = customerId;
            
           return _shoppingCartBusiness.Add(shoppingCart);
        }

        public ShoppingCart GetActiveShoppingCart(long customerId)
        {
            return _shoppingCartBusiness.GetActiveShoppingCart(customerId);
        }

        public bool UpdateStatus(long shoppingCartId, ShoppingCartStatus shoppingStatus)
        {
            return _shoppingCartBusiness.UpdateStatus(shoppingCartId, shoppingStatus);
        }

    }
}
