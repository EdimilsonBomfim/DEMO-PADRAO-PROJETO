using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Domain.Entities;
using WebShoes.Repository.Interfaces;

namespace WebShoes.Repository.Repository
{
    public class CreditCardRepository : BaseRepository<CreditCard>, ICreditCardRepository
    {
        private ICreditCardRepository _creditCardRepository;
        public CreditCard GetByReference(string reference)
        {
            return _creditCardRepository.GetByReference(reference);
        }
    }
}
