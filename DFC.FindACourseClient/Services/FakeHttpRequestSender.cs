using DFC.FindACourseClient.Contracts;
using System;
using System.Net.Http;

namespace DFC.FindACourseClient
{
    internal class FakeHttpRequestSender : IFakeHttpRequestSender
    {
        HttpResponseMessage IFakeHttpRequestSender.Send(HttpRequestMessage request)
        {
            throw new NotImplementedException("Now we can setup this method with our mocking framework");
        }
    }
}