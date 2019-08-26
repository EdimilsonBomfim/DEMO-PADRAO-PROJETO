using WebShoes.Domain.Entities;
using WebShoes.Domain.Entities.Values;

namespace WebShoes.Repository.Interfaces
{
    public interface IShoppingCartRepository : IBaseRepository<ShoppingCart>
    {
        ShoppingCart GetActiveShoppingCart(long customerId);

        bool UpdateStatus(long shoppingCartId, ShoppingCartStatus shoppingStatus);
    }
}
