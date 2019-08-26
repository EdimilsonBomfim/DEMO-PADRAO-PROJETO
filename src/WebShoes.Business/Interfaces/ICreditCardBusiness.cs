using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Domain.Entities;

namespace WebShoes.Business.Interfaces
{
    public interface ICreditCardBusiness
    {
        bool Add(long salesOrderId, String creditCard);
        CreditCard GetByReference(String reference);
        List<CreditCard> Select();
    }
}
