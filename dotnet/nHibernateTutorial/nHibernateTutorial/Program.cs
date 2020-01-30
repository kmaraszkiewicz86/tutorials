using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;

namespace nHibernateTutorial
{
    enum NHibernateConfigType
    {
        none = 0,
        cfgXml,
        appCnfig,
        code,
        fluentibeHibernate
    }

    class Program
    {
        static NHibernateConfigType _type = NHibernateConfigType.code;

        static ISessionFactory _sessionFactory;

        static void Main(string[] args)
        {
            Configuration nhConfig = new Configuration();
            
            switch (_type)
            {
                case NHibernateConfigType.cfgXml:
                    _sessionFactory = nhConfig.Configure().BuildSessionFactory();
                    break;

                case NHibernateConfigType.appCnfig:
                    _sessionFactory = nhConfig.Configure().BuildSessionFactory();
                    break;

                case NHibernateConfigType.code:
                    nhConfig = nhConfig.DataBaseIntegration(db =>
                    {
                        db.Dialect<MsSql2012Dialect>();
                        db.ConnectionStringName = "db";
                        db.BatchSize = 100;
                    });

                    _sessionFactory = nhConfig.BuildSessionFactory();
                    break;

                case NHibernateConfigType.fluentibeHibernate:

                    break;
            }

            Console.WriteLine("Nhibernate configured!");
            Console.ReadKey();
        }
    }
}
