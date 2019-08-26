using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Domain.Entities;

namespace WebShoes.Repository.Interfaces
{
    public interface IShoppingCartItemRepository : IBaseRepository<ShoppingCartItem>
    {
        List<ShoppingCartItem> GetShoppingCartItems(long shoppingCartId, bool active);

        void RemoveAll(long shoppingCartId);

        void RemoveProductQuantity(long shoppingCartId, long productId, int productQuantity);
    }
}
