using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Business.Interfaces;
using WebShoes.Domain.Entities;
using WebShoes.Services.Interfaces;

namespace WebShoes.Services.Services
{
    public class CreditCardService:ICreditCardService
    {
        private readonly ICreditCardBusiness _creditCardBusiness;

        public CreditCardService(ICreditCardBusiness creditCardBusiness)
        {
            _creditCardBusiness = creditCardBusiness;
        }
        public CreditCard GetByReference(string reference)
        {
            return _creditCardBusiness.GetByReference(reference);
        }
        public bool Insert(long salesOrderId, String creditCard)
        {

            return _creditCardBusiness.Add(salesOrderId, creditCard);

        }
        public List<CreditCard> Select()
        {
            return _creditCardBusiness.Select();
        }
    }
}
