using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Domain.Entities;

namespace WebShoes.Services.Interfaces
{
    public interface IShoppingCartItemService
    {
        bool Add(long shoppingCartId, long productId , int productQuantity);

        void RemoveAll(long shoppingCartId);

        void RemoveProductQuantity(long shoppingCartId, long productId, int productQuantity);
        
        List<ShoppingCartItem> GetShoppingCartItem(long shoppingCartId, bool active);
    }
}
