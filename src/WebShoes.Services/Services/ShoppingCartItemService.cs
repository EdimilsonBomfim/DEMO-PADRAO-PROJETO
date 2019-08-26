using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Business.Interfaces;
using WebShoes.Domain.Entities;
using WebShoes.Domain.Entities.Values;
using WebShoes.Services.Interfaces;

namespace WebShoes.Services.Services
{
    public class ShoppingCartItemService : IShoppingCartItemService
    {
        private readonly IShoppingCartItemBusiness _shoppingCartItemBusiness;

        public ShoppingCartItemService(IShoppingCartItemBusiness shoppingCartItemBusiness)
        {
            _shoppingCartItemBusiness = shoppingCartItemBusiness;
        }

        public bool Add(long shoppingCartId, long productId, int productQuantity)
        {
            var item = new ShoppingCartItem() { ShoppingCartId = shoppingCartId,
                ProductId = productId,
                ProductQuantity = productQuantity,
                Active = true
            };

            return _shoppingCartItemBusiness.Add(item);
        }

        public List<ShoppingCartItem> GetShoppingCartItem(long shoppingCartId, bool active)
        {
            return _shoppingCartItemBusiness.GetShoppingCartItems(shoppingCartId, active);
        }

        public void RemoveAll(long shoppingCartId)
        {
            _shoppingCartItemBusiness.RemoveAll(shoppingCartId);
        }

        public void RemoveProductQuantity(long shoppingCartId, long productId, int productQuantity)
        {
            _shoppingCartItemBusiness.RemoveProductQuantity(shoppingCartId, productId, productQuantity);
        }
    }
}
