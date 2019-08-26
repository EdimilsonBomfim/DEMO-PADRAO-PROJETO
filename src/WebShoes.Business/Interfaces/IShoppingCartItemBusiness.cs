using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Domain.Entities;

namespace WebShoes.Business.Interfaces
{
    public interface IShoppingCartItemBusiness
    {
        
        bool Add(ShoppingCartItem shoppingCartItem);

        List<ShoppingCartItem> GetShoppingCartItems(long shoppingCartId, bool active);

        void RemoveAll(long shoppingCartId);

        void RemoveProductQuantity(long shoppingCartId, long productId, int productQuantity);
    }
}
