using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook.Core.Helpers
{
    public class ConnectionStringHelper
    {
        public string ConnectionString =>
            ConfigurationManager.ConnectionStrings["GradeBook"].ConnectionString;
    }
}
