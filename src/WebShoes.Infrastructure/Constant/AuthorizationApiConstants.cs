using System;
using System.Collections.Generic;
using System.Text;

namespace WebShoes.Infrastructure.Constant
{
    public class AuthorizationApiConstants
    {
        public const string UrlPrincipal = "http://authorization-api.ddns.net:5000";

        public const string UrlCreditCardTransaction = UrlPrincipal + "/api/v1/CreditCardTransaction";

        public const string UrlPaymentSlipTransaction = UrlPrincipal + "/api/v1/PaymentSlipTransaction";
    }
}
