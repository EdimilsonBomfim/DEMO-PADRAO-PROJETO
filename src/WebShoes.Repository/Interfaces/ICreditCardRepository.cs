using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Domain.Entities;

namespace WebShoes.Repository.Interfaces
{
    public interface ICreditCardRepository : IBaseRepository<CreditCard>
    {
        CreditCard GetByReference(string reference);
    }
}
