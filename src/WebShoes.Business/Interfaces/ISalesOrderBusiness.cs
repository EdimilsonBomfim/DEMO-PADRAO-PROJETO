using System;
using System.Collections.Generic;
using WebShoes.Domain.Entities;
using WebShoes.Domain.Entities.Values;

namespace WebShoes.Business.Interfaces
{
    public interface ISalesOrderBusiness
    {
        IEnumerable<SalesOrder> GetByDate(DateTime date);
        bool Add(SalesOrder salesOrder);
        bool UpdateStatus(long salesOrderId, OrderStatus orderStatus);     
        SalesOrder GetById(long id);
        SalesOrder GetByShoppingCartId(long shoppingCartId);
    }
}
