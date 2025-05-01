using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace gamehub_API.Infrastructure.Models
{
    public class Videogame
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } // Cambiado a string para coincidir con el JSON

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "genre")]
        public string Genre { get; set; }

        [JsonProperty(PropertyName = "platform")]
        public string Platform { get; set; }

        [JsonProperty(PropertyName = "rating")]
        public string Rating { get; set; } // Cambiado a string para coincidir con el JSON

        [JsonProperty(PropertyName = "publisher")]
        public string Publisher { get; set; }

        [JsonProperty(PropertyName = "release")]
        public DateTime ReleaseDate { get; set; } // Cambiado el nombre de la propiedad para coincidir con el JSON

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }


    }
}
