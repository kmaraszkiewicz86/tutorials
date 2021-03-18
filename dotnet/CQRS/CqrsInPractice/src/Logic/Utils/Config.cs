namespace Logic.Utils
{
    public class Config
    {
        public int NumberOfDatabaseRetires { get; }

        public Config(int numberOfDatabaseRetires)
        {
            NumberOfDatabaseRetires = numberOfDatabaseRetires;
        }
    }
}