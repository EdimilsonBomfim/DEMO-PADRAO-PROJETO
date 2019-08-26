using System;
using System.Collections.Generic;
using WebShoes.Business.Interfaces;
using WebShoes.Domain.Entities;
using WebShoes.Repository.Interfaces;

namespace WebShoes.Business.Business
{
    public class ProductBusiness : IProductBusiness
    { 
        private readonly IProductRepository _productRepository;
        public ProductBusiness(IProductRepository productrepository)
        {
            _productRepository = productrepository;
        }
        public bool Add(Product product)
        {
            return _productRepository.Insert(product);
        }
        public bool IncreaseQuantity(long id, int quantity)
        {
            try
            {
                Product product = GetById(id);
                product.Quantity += quantity;
                _productRepository.Update(product);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DecreaseQuantity(long id, int quantity)
        {
            try
            {
                Product product = GetById(id);
                if (product.Quantity >= quantity)
                {
                    product.Quantity -= quantity;
                    _productRepository.Update(product);
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Product> GetAll()
        {
            return _productRepository.Select();
        }

        public Product GetById(long id)
        {
            return _productRepository.Select(id);
        }

        public string GetDescription(long id)
        {
            return _productRepository.GetDescription(id);
        }

        public int GetStock(long id)
        {
            return _productRepository.GetStock(id);
        }

        public int GetUnitPrice(long id)
        {
            return _productRepository.GetUnitPrice(id);
        }
               
        public bool ChangeUnitPrice(long id, int unitprice)
        {
            try
            {
                Product product = GetById(id);
                product.UnitPrice = unitprice;
                _productRepository.Update(product);
                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }
    }
}
