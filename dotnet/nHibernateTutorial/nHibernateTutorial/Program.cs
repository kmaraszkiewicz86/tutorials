using NHibernate;
using nHibernateTutorial.Core;
using nHibernateTutorial.Enums;
using System;

namespace nHibernateTutorial
{
    class Program
    {
        static NHibernateConfigType _type = NHibernateConfigType.fluentibeHibernate;

        static ISessionFactory _sessionFactory;

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            switch (_type)
            {
                case NHibernateConfigType.cfgXml:
                    _sessionFactory = NHibernateInitilization.ConfigureFromConfigFile();
                    break;

                case NHibernateConfigType.appCnfig:
                    _sessionFactory = NHibernateInitilization.ConfigureFromConfigFile();
                    break;

                case NHibernateConfigType.code:
                    _sessionFactory = NHibernateInitilization.ConfgigurationFromCode();
                    break;

                case NHibernateConfigType.fluentibeHibernate:
                    _sessionFactory = FluentHibernateInitiliazation.ConfigureFromCode();
                    break;
            }

            Console.WriteLine("Nhibernate configured!");

            Console.Write("Log4Net testing configuration");

            Log4NetExample.DoTestThing();

            Console.ReadKey();
        }
    }
}
