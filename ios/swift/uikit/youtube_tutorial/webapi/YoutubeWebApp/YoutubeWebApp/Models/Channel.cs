using System.Text.Json.Serialization;

namespace YoutubeWebApp.Models
{
    public class Channel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("profile_image_name")]
        public string ProfileImageName { get; set; }
    }
}
