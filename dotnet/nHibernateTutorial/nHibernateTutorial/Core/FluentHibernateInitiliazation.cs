using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace nHibernateTutorial.Core
{
    class FluentHibernateInitiliazation
    {
        public static ISessionFactory ConfigureFromCode()
        {
            var config = MsSqlConfiguration
                            .MsSql2012
                            .ConnectionString(conn => conn.FromConnectionStringWithKey("db"))
                            .AdoNetBatchSize(100);

            return Fluently.Configure().Database(config)
                .BuildSessionFactory();
        }
    }
}
