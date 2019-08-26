using RestSharp;
using WebShoes.Infrastructure.Interface;

namespace WebShoes.Infrastructure.Factory
{
    public class HttpFactory : IHttpFactory
    {
        public IRestResponse RestHttp(string url, RestRequest request)
        {
            //create RestSharp client and POST request object
            var client = new RestClient(url);
            
            // Common fields for every request
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");

            //make the API request and return a response
            return client.Execute(request);
        }
    }
}
