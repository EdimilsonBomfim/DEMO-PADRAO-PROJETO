using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Linq;
using WebShoes.Domain.Entities.Abstract;
using WebShoes.Domain.Entities.Values;

namespace WebShoes.Domain.Entities
{
    [Table("SalesOrder")]
    public class SalesOrder : BaseEntity
    {        
        public SalesOrder()
        {
            ListSalesOrderItem = new List<SalesOrderItem>();
        }
        
        public long ShoppingCartId { get; set; }
        public long CustomerId { get; set; }
        public OrderStatus OrderStatus { get; set; }

        [Computed]
        public IList<SalesOrderItem> ListSalesOrderItem { get; set; }

        [Computed]
        public int TotalPrice { get => ListSalesOrderItem.Sum(s => s.ProductQuantity * s.UnitPrice); }
    }
}
