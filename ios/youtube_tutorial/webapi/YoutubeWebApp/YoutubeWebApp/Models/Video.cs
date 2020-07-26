namespace YoutubeWebApp.Models
{
    public class Video
    {
        public string Title { get; set; }

        public int NumberOfViews { get; set; }

        public string ThumbnailImageName { get; set; }

        public Channel Channel { get; set; }

        public int Duration { get; set; }
    }
}
