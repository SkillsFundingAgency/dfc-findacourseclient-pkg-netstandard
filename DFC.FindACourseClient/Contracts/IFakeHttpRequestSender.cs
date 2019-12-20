using System.Net.Http;

namespace DFC.FindACourseClient.Contracts
{
    internal interface IFakeHttpRequestSender
    {
        HttpResponseMessage Send(HttpRequestMessage request);
    }
}