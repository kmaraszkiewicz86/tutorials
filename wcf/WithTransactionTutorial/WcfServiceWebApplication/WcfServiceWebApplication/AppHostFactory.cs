using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Autofac.Integration.Wcf;
using Core.Services.PersonServiceImpl;

namespace WcfServiceWebApplication
{
    public class AppHostFactory: ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = new ServiceHost(serviceType, baseAddresses);

            return host;
        }
    }
}