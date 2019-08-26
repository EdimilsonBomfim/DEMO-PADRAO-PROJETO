using System;
using System.Collections.Generic;
using WebShoes.Domain.Entities;
using WebShoes.Domain.Entities.Values;

namespace WebShoes.Services.Interfaces
{
    public interface ISalesOrderServices
    {        
        IEnumerable<SalesOrder> GetByDate(DateTime date);
        bool Add(long customerId);
        bool UpdateStatus(long salesOrderId, OrderStatus orderStatus);
        SalesOrder GetById(long id);
        SalesOrder GetByShoppingCartId(long shoppingCartId);
    }
}
