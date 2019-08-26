using RestSharp;

namespace WebShoes.Infrastructure.Interface
{
    public interface IHttpFactory
    {
        IRestResponse RestHttp(string Url, RestRequest Request);
    }
}
