using System;
using System.Linq;
using System.Threading.Tasks;

namespace NewFeaturesOnNet5.Data
{
    public class WeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            var rng = new Random();
            var results = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55)
            }).ToArray();

            //relationl pattern matching is >, <, >=, <=
            //logical pattern matching is and, or, & not
            foreach (var rec in results)
            {
                //switch expression
                rec.Summary = rec.TemperatureC switch
                {
                    < -32 => "Well Below Freezing",
                    >= -32 and < 0 => "Freezing",
                    0 or 80 => "Exactly Freezing or Boiling",
                    //0 => "Exactly Freezing",
                    > 0 and < 20 => "Cool",
                    >= 20 and < 25 => "Warm",
                    >= 25 => "Hot"
                };
            }

            return Task.FromResult(results);
        }
    }
}
