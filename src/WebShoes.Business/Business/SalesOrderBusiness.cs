using System;
using System.Collections.Generic;
using WebShoes.Business.Interfaces;
using WebShoes.Domain.Entities;
using WebShoes.Domain.Entities.Values;
using WebShoes.Repository.Interfaces;

namespace WebShoes.Business.Business
{
    public class SalesOrderBusiness : ISalesOrderBusiness
    {
        private readonly ISalesOrderRepository _salesOrderRepository;

        public SalesOrderBusiness(ISalesOrderRepository salesOrderRepository)
        {
            _salesOrderRepository = salesOrderRepository;
        }

        public bool Add(SalesOrder salesOrder)
        {
            return _salesOrderRepository.Add(salesOrder);
        }        

        public IEnumerable<SalesOrder> GetByDate(DateTime date)
        {
            return _salesOrderRepository.GetByDate(date);
        }

        public bool UpdateStatus(long salesOrderId, OrderStatus orderStatus)
        {
            return _salesOrderRepository.UpdateStatus(salesOrderId, orderStatus);
        }

        public SalesOrder GetById(long id)
        {
            return _salesOrderRepository.Select(id);
        }

        public SalesOrder GetByShoppingCartId(long shoppingCartId)
        {
            return _salesOrderRepository.GetByShoppingCartId(shoppingCartId);
        }
    }
}
