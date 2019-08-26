using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Domain.Entities;

namespace WebShoes.Repository.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    { 
        int GetUnitPrice(long id);
        int GetStock(long id);
        string GetDescription(long id);
    }
}
