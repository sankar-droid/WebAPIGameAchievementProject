using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace WebAPIProject.Models
{
    public class GameGenre
    {
        [Key]
        [MaxLength(20)]
        public string GameGenreId { get; set; }

        [Required, MaxLength(50)]
        public string GenreName { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }


        [MaxLength(50)]
        public string Popularity { get; set; }

        [JsonIgnore]
        public ICollection<Game> Games { get; set; }
    }
}
