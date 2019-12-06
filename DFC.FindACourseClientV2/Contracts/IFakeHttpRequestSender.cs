using System.Net.Http;

namespace DFC.FindACourseClientV2.Contracts
{
    public interface IFakeHttpRequestSender
    {
        HttpResponseMessage Send(HttpRequestMessage request);
    }
}