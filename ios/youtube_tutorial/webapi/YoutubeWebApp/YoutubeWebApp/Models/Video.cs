using System.Text.Json.Serialization;

namespace YoutubeWebApp.Models
{
    public class Video
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("number_of_views")]
        public int NumberOfViews { get; set; }

        [JsonPropertyName("thumbnail_image_name")]
        public string ThumbnailImageName { get; set; }

        [JsonPropertyName("channel")]
        public Channel Channel { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }
    }
}
