using System;
using System.Collections.Generic;
using WebShoes.Domain.Entities;
using WebShoes.Domain.Entities.Values;

namespace WebShoes.Repository.Interfaces
{
    public interface ISalesOrderRepository : IBaseRepository<SalesOrder>
    {
        bool Add(SalesOrder salesOrder);
        IEnumerable<SalesOrder> GetByDate(DateTime date);        
        bool UpdateStatus(long salesOrderId, OrderStatus orderStatus);
        SalesOrder GetByShoppingCartId(long shoppingCartId);
    }
}
