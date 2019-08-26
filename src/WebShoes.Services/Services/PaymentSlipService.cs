using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Business.Interfaces;
using WebShoes.Domain.Entities;
using WebShoes.Services.Interfaces;

namespace WebShoes.Services.Services
{
    public class PaymentSlipService : IPaymentSlipService
    {
        private readonly IPaymentSlipBusiness _paymentSlipBusiness;

        public PaymentSlipService(IPaymentSlipBusiness paymentSlipBusiness)
        {
            _paymentSlipBusiness = paymentSlipBusiness;
        }
        public PaymentSlip GetByReference(string reference)
        {
            return _paymentSlipBusiness.GetByReference(reference);
        }
        public bool Insert(long salesOrderId, String paymentslip)
        {

            return _paymentSlipBusiness.Add(salesOrderId, paymentslip);

        }
        public List<PaymentSlip> Select()
        {
            return _paymentSlipBusiness.Select();
        }
    }
}
