using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

namespace DFC.FindACourseClient.Contracts
{
    public interface IServiceClientProxy
    {
        IClientChannel CreateChannel(ChannelFactory<ServiceInterface> channelFactory);
    }
}