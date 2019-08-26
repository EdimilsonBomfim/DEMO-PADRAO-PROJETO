using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Domain.Entities;

namespace WebShoes.Repository.Interfaces
{
    public interface IPaymentSlipRepository : IBaseRepository<PaymentSlip>
    {
        PaymentSlip GetByReference(string reference);
    }
}
