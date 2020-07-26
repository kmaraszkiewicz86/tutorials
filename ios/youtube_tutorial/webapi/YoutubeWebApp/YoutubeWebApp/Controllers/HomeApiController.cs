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

            return new List<Video>
            {
                new Video
                {
                    Title = "Pierdki - Se siedzą, sie gapia",
                    NumberOfViews = 1300923900,
                    ThumbnailImageName = $"{imagesPath}/dogs1.jpeg",
                    Channel = channel,
                    Duration = 410
                },
                new Video
                {
                    Title = "Pierdki - Se stoja, sie gapia na pike, ktora jest u gory na suficie",
                    NumberOfViews = 11111111,
                    ThumbnailImageName = $"{imagesPath}/dogs2.jpeg",
                    Channel = channel,
                    Duration = 410
                },
                new Video
                {
                    Title = "Pierdki - Kotopitbulopies po kapieli",
                    NumberOfViews = 1300923900,
                    ThumbnailImageName = $"{imagesPath}/dogs3.jpeg",
                    Channel = channel,
                    Duration = 410
                }
            };

        }
    }
}