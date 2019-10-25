using DFC.FindACourseClient.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

namespace DFC.FindACourseClient
{
    public class ServiceClientProxy : IServiceClientProxy
    {
        public IClientChannel CreateChannel(ChannelFactory<ServiceInterface> channelFactory)
        {
            return (IClientChannel)channelFactory?.CreateChannel();
        }
    }
}
