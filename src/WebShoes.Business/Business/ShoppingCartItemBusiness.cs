using System;
using System.Collections.Generic;
using WebShoes.Business.Interfaces;
using WebShoes.Domain.Entities;
using WebShoes.Repository.Interfaces;

namespace WebShoes.Business.Business
{
    public class ShoppingCartItemBusiness : IShoppingCartItemBusiness
    {
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IProductBusiness _productBusiness;

        public ShoppingCartItemBusiness(IShoppingCartItemRepository shoppingCartItemRepository, IProductBusiness productBusiness)
        {
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _productBusiness = productBusiness;
        }

        public bool Add(ShoppingCartItem shoppingCartItem)
        {
            // valida quantidade
            if(shoppingCartItem.ProductQuantity <= 0)
            {
                throw new ArgumentException($"Quantidade invalida");
            }

            
           if (_productBusiness.GetById(shoppingCartItem.ProductId) == null)
                throw new ArgumentException($"Produto não encontrado : {shoppingCartItem.ProductId}");


            return _shoppingCartItemRepository.Insert(shoppingCartItem);
        }

        public List<ShoppingCartItem> GetShoppingCartItems(long shoppingCartId, bool active)
        {
            return _shoppingCartItemRepository.GetShoppingCartItems(shoppingCartId, active);
        }

        public void RemoveAll(long shoppingCartId)
        {
            _shoppingCartItemRepository.RemoveAll(shoppingCartId);
        }

        public void RemoveProductQuantity(long shoppingCartId, long productId, int productQuantity)
        {
            _shoppingCartItemRepository.RemoveProductQuantity(shoppingCartId, productId, productQuantity);
        }
    }
}
