using System.Net.Http;

namespace DFC.FindACourseClient
{
    public interface IHttpClientService
    {
        HttpClient GetClient();
    }
}