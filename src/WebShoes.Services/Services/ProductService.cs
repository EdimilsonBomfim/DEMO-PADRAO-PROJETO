using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Business.Interfaces;
using WebShoes.Domain.Entities;
using WebShoes.Service.Interfaces;

namespace WebShoes.Services.Services
{

    public class ProductService : IProductService
    { 
        private readonly IProductBusiness _productBusiness;
        public ProductService(IProductBusiness productbusiness)
        {
            _productBusiness = productbusiness;
        }

        public bool Add(string code, string description, int quantity, int unitprice)
        {
            //Criando objeto Produto
            Product product = new Product();
            //Atribuindo valores para a criação do novo produto.
            product.Code = code;
            product.Description = description;
            product.Quantity = quantity;
            product.UnitPrice = unitprice;
            //
            return _productBusiness.Add(product);
        }

        public bool DecreaseQuantity(long id, int quantity)
        {
            return _productBusiness.DecreaseQuantity(id, quantity);
        }

        public List<Product> GetAll()
        {
            return _productBusiness.GetAll();
        }

        public Product GetById(long id)
        {
            return _productBusiness.GetById(id);
        }

        public string GetDescription(long id)
        {
            return _productBusiness.GetDescription(id);
        }

        public int GetStock(long id)
        {
            return _productBusiness.GetStock(id);
        }

        public int GetUnitPrice(long id)
        {
            return _productBusiness.GetUnitPrice(id);
        }

        public bool IncreaseQuantity(long id, int quantity)
        {
            return _productBusiness.IncreaseQuantity(id, quantity);
        }
        public bool ChangeUnitPrice(long id, int unitprice)
        {
            return _productBusiness.ChangeUnitPrice(id, unitprice);
        }
    }
}
