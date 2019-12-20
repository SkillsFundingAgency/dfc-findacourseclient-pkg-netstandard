using DFC.FindACourseClient.Contracts;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("DFC.FindACourseClient.UnitTests")]

namespace DFC.FindACourseClient.UnitTests.ClientHandlers
{
    internal class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly IFakeHttpRequestSender fakeHttpRequestSender;

        public FakeHttpMessageHandler(IFakeHttpRequestSender fakeHttpRequestSender)
        {
            this.fakeHttpRequestSender = fakeHttpRequestSender;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(fakeHttpRequestSender.Send(request));
        }
    }
}