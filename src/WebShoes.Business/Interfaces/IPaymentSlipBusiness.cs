using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Domain.Entities;

namespace WebShoes.Business.Interfaces
{
    public interface IPaymentSlipBusiness
    {
        bool Add(long salesOrderId, String paymentSlip);
        PaymentSlip GetByReference(String reference);
        List<PaymentSlip> Select();
    }
}
