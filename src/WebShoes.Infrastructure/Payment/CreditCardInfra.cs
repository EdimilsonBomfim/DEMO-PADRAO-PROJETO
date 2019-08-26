using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using WebShoes.Infrastructure.Model;
using Newtonsoft.Json;
using WebShoes.Infrastructure.Interface;
using WebShoes.Infrastructure.Constant;

namespace WebShoes.Infrastructure.Payment
{
    public class CreditCardInfra : ICreditCardInfra
    {
        private readonly IHttpFactory _httpFactory;

        public CreditCardInfra(IHttpFactory httpFactory)
        {
            _httpFactory = httpFactory;
        }

        public CreditCardInfraModel CreditCard(string json)
        {
            var request = new RestRequest(Method.POST);
         
            request.AddParameter("undefined", json, ParameterType.RequestBody);

            var response = _httpFactory.RestHttp(AuthorizationApiConstants.UrlCreditCardTransaction, request);

            return JsonConvert.DeserializeObject<CreditCardInfraModel>(response.Content);
        }
    }
}
