using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;

namespace nHibernateTutorial.Core
{
    static class NHibernateInitilization
    {
        private static Configuration _nhConfig = new Configuration();
        
        public static ISessionFactory ConfigureFromConfigFile()
        {
            return _nhConfig.Configure().BuildSessionFactory();
        }

        public static ISessionFactory ConfgigurationFromCode()
        {
            _nhConfig = _nhConfig.DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2012Dialect>();
                db.ConnectionStringName = "db";
                db.BatchSize = 100;
            });

            return _nhConfig.BuildSessionFactory();
        }
    }
}
