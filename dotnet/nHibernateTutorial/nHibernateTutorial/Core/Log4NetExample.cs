using log4net;
using System.IO;

namespace nHibernateTutorial.Core
{
    public static class Log4NetExample
    {
        //private static readonly log4net.ILog Log
        //    = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly ILog Log = LogManager.GetLogger(typeof(Log4NetExample));

        public static string DoTestThing()
        {
            Log.Debug("We're doing something");

            try
            {
                return File.ReadAllText("cheese.txt");
            }
            catch (FileNotFoundException e)
            {
                Log.Error("Somebody cheese.txt");
                return null;
            }
        }
    }
}
