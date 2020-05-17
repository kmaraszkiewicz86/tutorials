using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Autofac;
using Autofac.Integration.Wcf;
using PersonalInfomationWebService.ServiceImplementations;
using PersonalInfomationWebService.Services.Implementations;
using PersonalInfomationWebService.Services.Interfaces;

namespace PersonalInfomationWebService
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<PersonalInforamtionService>();
            builder.RegisterType<PeopleService>().As<IPeopleService>();

            var container = builder.Build();
            AutofacHostFactory.Container = container;
        }
    }
}