using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Business.Interfaces;
using WebShoes.Domain.Entities;
using WebShoes.Infrastructure.Interface;
using WebShoes.Repository.Interfaces;
namespace WebShoes.Business.Business
{
    public class CreditCardBusiness : ICreditCardBusiness
    {
        private ICreditCardRepository _creditCardRepository;
        private ICreditCardInfra _creditCardInfra;
        private readonly ISalesOrderBusiness _salesOrderBusiness;

        public CreditCardBusiness(ICreditCardRepository creditCardRepository, ICreditCardInfra creditCardInfra, ISalesOrderBusiness salesOrderBusiness)
        {
            _creditCardRepository = creditCardRepository;
            _creditCardInfra = creditCardInfra;
            _salesOrderBusiness = salesOrderBusiness;
        }

        public bool Add(long salesorderid, string creditCard)
        {
            var objinfra = _creditCardInfra.CreditCard(creditCard);

            var obj = new CreditCard()
            {
                Reference = objinfra.creditCardTransaction.reference,
                AmountInCents = objinfra.creditCardTransaction.amountInCents,
                AuthorizedAt = objinfra.creditCardTransaction.authorizedAt,
                SalesOrderId = salesorderid
            };


            _salesOrderBusiness.UpdateStatus(salesorderid, Domain.Entities.Values.OrderStatus.PaymentApproved);

            return _creditCardRepository.Insert(obj);
        }

        public CreditCard GetByReference(string reference)
        {
            return _creditCardRepository.GetByReference(reference);
        }

        public List<CreditCard> Select()
        {
            return _creditCardRepository.Select();
        }
    }
}
