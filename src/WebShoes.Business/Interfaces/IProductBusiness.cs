using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Domain.Entities;

namespace WebShoes.Business.Interfaces
{
    public interface IProductBusiness
    { 
        bool Add(Product product);
        int GetUnitPrice(long id);
        int GetStock(long id);
        string GetDescription(long id);
        bool DecreaseQuantity(long id, int quantity);
        bool IncreaseQuantity(long id, int quantity);
        List<Product> GetAll();
        Product GetById(long id);
        bool ChangeUnitPrice(long id, int unitprice);
    }
}
