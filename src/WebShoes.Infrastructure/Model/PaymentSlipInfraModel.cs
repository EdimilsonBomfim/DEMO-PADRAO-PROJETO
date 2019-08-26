using System;
using System.Collections.Generic;
using System.Text;

namespace WebShoes.Infrastructure.Model
{
    public class PaymentSlipInfraModel
    {
        public PaymentSlipTransaction paymentSlipTransaction { get; set; }
        public string requestKey { get; set; }
        public DateTime createDate { get; set; }
        public double internalTimeMs { get; set; }

    }
    public class PaymentSlip
    {
        public DateTime dueDate { get; set; }
        public string barCode { get; set; }
        public int status { get; set; }
        public int id { get; set; }
        public string key { get; set; }
    }

    public class PaymentSlipTransaction
    {
        public string reference { get; set; }
        public string affiliationKey { get; set; }
        public int amountInCents { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime authorizedAt { get; set; }
        public PaymentSlip paymentSlip { get; set; }
        public int id { get; set; }
        public string key { get; set; }
    }
}
