using System;
using WebShoes.Domain.Entities;
using WebShoes.Repository.Interfaces;

namespace WebShoes.Repository.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {        
        public string GetDescription(long id)
        {
            try
            {
                return Select(id).Description;
            }
            catch (Exception)
            {
                return "Product not found";
            }
        }

        public int GetUnitPrice(long id)
        {
            try
            {
                return Select(id).UnitPrice;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int GetStock(long id)
        {
            try
            {
                return Select(id).Quantity;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
