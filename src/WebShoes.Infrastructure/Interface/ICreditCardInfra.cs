using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Infrastructure.Model;

namespace WebShoes.Infrastructure.Interface
{
    public interface ICreditCardInfra
    {
        CreditCardInfraModel CreditCard(String json);
    }
}
