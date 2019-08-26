using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Domain.Entities;

namespace WebShoes.Services.Interfaces
{
    public interface IPaymentSlipService
    {
        bool Insert(long salesOrderId, String paymentSlip);
        List<PaymentSlip> Select();
        PaymentSlip GetByReference(string reference);

    }
}
