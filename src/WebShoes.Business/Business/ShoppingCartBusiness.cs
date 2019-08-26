using System;
using WebShoes.Business.Interfaces;
using WebShoes.Domain.Entities;
using WebShoes.Domain.Entities.Values;
using WebShoes.Repository.Interfaces;

namespace WebShoes.Business.Business
{
    public class ShoppingCartBusiness : IShoppingCartBusiness
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICustormeBusiness _customerBusiness;

        public ShoppingCartBusiness(IShoppingCartRepository shoppingCartRepository, ICustormeBusiness custormeBusiness)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _customerBusiness = custormeBusiness;
        }

        public bool Add(ShoppingCart shoppingCart)
        {
            if( _customerBusiness.Select(shoppingCart.CustomerId) == null )
                throw new ArgumentException($"Cliente não encontrado : {shoppingCart.CustomerId}");
            
            return _shoppingCartRepository.Insert(shoppingCart);
        }

        public bool UpdateStatus(long shoppingCartId, ShoppingCartStatus shoppingStatus)
        {
            if (_shoppingCartRepository.Select(shoppingCartId) == null)
                throw new ArgumentException($"Carrinho não encontrado : {shoppingCartId}");

            return _shoppingCartRepository.UpdateStatus(shoppingCartId, shoppingStatus);
        }

        public ShoppingCart GetActiveShoppingCart(long customerId)
        {
            return _shoppingCartRepository.GetActiveShoppingCart(customerId);
        }
    }
}
