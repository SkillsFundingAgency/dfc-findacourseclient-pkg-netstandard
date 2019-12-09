using System.Net.Http;

namespace DFC.FindACourseClient.Contracts
{
    public interface IFakeHttpRequestSender
    {
        HttpResponseMessage Send(HttpRequestMessage request);
    }
}