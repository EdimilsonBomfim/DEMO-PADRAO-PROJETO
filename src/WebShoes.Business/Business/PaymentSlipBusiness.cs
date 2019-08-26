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
    public class PaymentSlipBusiness:IPaymentSlipBusiness
    {
        private IPaymentSlipRepository _paymentSlipRepository;
        private IPaymentSlipInfra _paymentSlipInfra;
        private readonly ISalesOrderBusiness _salesOrderBusiness;
        public PaymentSlipBusiness(IPaymentSlipRepository paymentSlipRepository, IPaymentSlipInfra paymentSlipInfra, ISalesOrderBusiness salesOrderBusiness)
        {
            _paymentSlipRepository = paymentSlipRepository;
            _paymentSlipInfra = paymentSlipInfra;
            _salesOrderBusiness = salesOrderBusiness;
        }

        public bool Add(long salesorderid, string paymentSlip)
        {
            var objinfra = _paymentSlipInfra.PaymentSlip(paymentSlip);

            var obj = new PaymentSlip() {
                DueDate = objinfra.paymentSlipTransaction.paymentSlip.dueDate,
                BarCode = objinfra.paymentSlipTransaction.paymentSlip.barCode,
                Status = objinfra.paymentSlipTransaction.paymentSlip.status,
                TransactionId = objinfra.paymentSlipTransaction.paymentSlip.id,
                TransactionKey = objinfra.paymentSlipTransaction.paymentSlip.key,
                SalesOrderId = salesorderid};

            _salesOrderBusiness.UpdateStatus(salesorderid, Domain.Entities.Values.OrderStatus.PaymentApproved);
            
            return _paymentSlipRepository.Insert(obj);
        }

        public PaymentSlip GetByReference(string reference)
        {
            return _paymentSlipRepository.GetByReference(reference);
        }

        public List<PaymentSlip> Select()
        {
            return _paymentSlipRepository.Select();
        }
    }
}
