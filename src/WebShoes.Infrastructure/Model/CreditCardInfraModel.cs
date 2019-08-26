using System;
using System.Collections.Generic;
using System.Text;

namespace WebShoes.Infrastructure.Model
{
   public class CreditCardInfraModel
    {
        public CreditCardTransaction creditCardTransaction { get; set; }
        public string requestKey { get; set; }
        public DateTime createDate { get; set; }
        public double internalTimeMs { get; set; }
    }

    public class CreditCard
    {
        public string branch { get; set; }
        public string number { get; set; }
        public string expirationDate { get; set; }
        public string securityCode { get; set; }
        public string holderName { get; set; }
        public int status { get; set; }
        public int id { get; set; }
        public string key { get; set; }
    }

    public class CreditCardTransaction
    {
        public string affiliationKey { get; set; }
        public string reference { get; set; }
        public int amountInCents { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime authorizedAt { get; set; }
        public CreditCard creditCard { get; set; }
        public int id { get; set; }
        public string key { get; set; }
    }

}
