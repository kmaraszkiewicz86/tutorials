using System.Configuration;

namespace GradeBook.Core.Helpers
{
    public static class ConnectionStringHelper
    {
        public static string ConnectionString =>
            ConfigurationManager.ConnectionStrings["GradeBook"].ConnectionString;
    }
}
