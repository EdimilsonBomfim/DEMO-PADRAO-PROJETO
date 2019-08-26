using WebShoes.Domain.Entities;
using WebShoes.Domain.Entities.Values;

namespace WebShoes.Services.Interfaces
{
    public interface IShoppingCartService
    {
        bool Add(long customerId);
        ShoppingCart GetActiveShoppingCart(long customerId);
        bool UpdateStatus(long shoppingCartId, ShoppingCartStatus shoppingStatus);
    }
}
