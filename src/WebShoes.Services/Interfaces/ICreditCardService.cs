using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Domain.Entities;

namespace WebShoes.Services.Interfaces
{
    public interface ICreditCardService
    {
        bool Insert(long salesOrderId, String creditCard);
        List<CreditCard> Select();
        CreditCard GetByReference(string reference);
    }
}
