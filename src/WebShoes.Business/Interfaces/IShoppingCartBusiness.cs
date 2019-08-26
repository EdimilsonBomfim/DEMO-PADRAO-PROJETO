using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Domain.Entities;
using WebShoes.Domain.Entities.Values;

namespace WebShoes.Business.Interfaces
{
    public interface IShoppingCartBusiness
    {
        
        bool Add(ShoppingCart shoppingCart);
        ShoppingCart GetActiveShoppingCart(long customerId);
        bool UpdateStatus(long shoppingCartId, ShoppingCartStatus shoppingStatus);
    }
}
