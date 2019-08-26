using Newtonsoft.Json;
using RestSharp;
using WebShoes.Infrastructure.Constant;
using WebShoes.Infrastructure.Interface;
using WebShoes.Infrastructure.Model;

namespace WebShoes.Infrastructure.Payment
{
    public class PaymentSlipInfra: IPaymentSlipInfra
    {
        private readonly IHttpFactory _httpFactory;

        public PaymentSlipInfra(IHttpFactory httpFactory)
        {
            _httpFactory = httpFactory;

        }

        public PaymentSlipInfraModel PaymentSlip(string json)
        {
            var request = new RestRequest(Method.POST);

            request.AddParameter("undefined", json, ParameterType.RequestBody);

            var response = _httpFactory.RestHttp(AuthorizationApiConstants.UrlPaymentSlipTransaction, request);

            return JsonConvert.DeserializeObject<PaymentSlipInfraModel>(response.Content);
        }
    }
}
