using NHibernate;
using nHibernateTutorial.Core;
using nHibernateTutorial.Enums;
using System;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using nHibernateTutorial.Models;

namespace nHibernateTutorial
{
    class Program
    {
        static NHibernateConfigType _type = NHibernateConfigType.appCnfig;

        static ISessionFactory _sessionFactory;

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            var nhConfig = new Configuration().Configure();
            var mapper = new ConventionModelMapper();
            nhConfig.AddMapping(mapper.CompileMappingFor(new [] { typeof(TestClass) }));

            var schemaExport = new SchemaExport(nhConfig);
            schemaExport.Create(false, true);

            //switch (_type)
            //{
            //    case NHibernateConfigType.cfgXml:
            //    case NHibernateConfigType.appCnfig:
            //        _sessionFactory = NHibernateInitilization.ConfigureFromConfigFile();
            //        break;

            //    case NHibernateConfigType.code:
            //        _sessionFactory = NHibernateInitilization.ConfgigurationFromCode();
            //        break;

            //    case NHibernateConfigType.fluentibeHibernate:
            //        _sessionFactory = FluentHibernateInitiliazation.ConfigureFromCode();
            //        break;
            //}

            Console.WriteLine("Nhibernate configured!");

            Console.Write("Log4Net testing configuration");

            Log4NetExample.DoTestThing();

            Console.ReadKey();
        }
    }
}
