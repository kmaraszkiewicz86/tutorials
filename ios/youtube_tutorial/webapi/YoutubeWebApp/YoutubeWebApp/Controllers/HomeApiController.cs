using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using YoutubeWebApp.Models;

namespace YoutubeWebApp.Controllers
{
    [Route("api/Home")]
    [ApiController]
    public class HomeApiController : ControllerBase
    {
        private IWebHostEnvironment _hostingEnvironment;

        public HomeApiController(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        public IEnumerable<Video> GetAll()
        {


            var imagesPath = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/Images";

            var channel = new Channel
            {
                Name = "Szarko",
                ProfileImageName = $"{imagesPath}/szarko.jpeg"
            };

            var channel1 = new Channel
            {
                Name = "Izi",
                ProfileImageName = $"{imagesPath}/izi.jpeg"
            };

            var channel2 = new Channel
            {
                Name = "Mailo",
                ProfileImageName = $"{imagesPath}/mailo.jpeg"
            };

            return new List<Video>
            {
                new Video
                {
                    Title = $"{channel.Name} - Se siedzą, sie gapia",
                    NumberOfViews = 1300923900,
                    ThumbnailImageName = $"{imagesPath}/dogs1.jpeg",
                    Channel = channel,
                    Duration = 199
                },
                new Video
                {
                    Title = $"{channel1.Name} - Se stoja, sie gapia na pike, ktora jest u gory na suficie",
                    NumberOfViews = 11111111,
                    ThumbnailImageName = $"{imagesPath}/dogs2.jpeg",
                    Channel = channel1,
                    Duration = 200
                },
                new Video
                {
                    Title = $"{channel2.Name} - Kotopitbulopies zrobiła świnstwo",
                    NumberOfViews = 1,
                    ThumbnailImageName = $"{imagesPath}/dogs3.jpeg",
                    Channel = channel2,
                    Duration = 123
                },
                new Video
                {
                    Title = $"{channel.Name} - Ta tutaj i ten u góry",
                    NumberOfViews = 12,
                    ThumbnailImageName = $"{imagesPath}/dogs4.jpeg",
                    Channel = channel,
                    Duration = 300
                },
                new Video
                {
                    Title = $"{channel1.Name} - Krowiej na rencach",
                    NumberOfViews = 1300923900,
                    ThumbnailImageName = $"{imagesPath}/dogs5.jpeg",
                    Channel = channel1,
                    Duration = 111
                },
                new Video
                {
                    Title = $"{channel2.Name} - Kurki na rencach",
                    NumberOfViews = 3,
                    ThumbnailImageName = $"{imagesPath}/dogs6.jpeg",
                    Channel = channel2,
                    Duration = 222
                }
            };

        }
    }
}