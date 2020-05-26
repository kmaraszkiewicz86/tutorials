using Autofac;
using Autofac.Integration.Wcf;
using Core.Services.PersonServiceImpl;
using System;
using WcfServiceWebApplication.ServiceContracts.PersonServiceContract;

namespace WcfServiceWebApplication
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<PersonService>().As<IPersonService>();
            builder.RegisterType<PersonServiceContract>();

            AutofacHostFactory.Container = builder.Build();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}