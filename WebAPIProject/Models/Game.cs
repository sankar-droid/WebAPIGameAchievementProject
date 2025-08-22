using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIProject.Models
{
    public class Game
    {
        [Key]
        [MaxLength(20)]
        public string GameId { get; set; }

        [Required, MaxLength(100)]
        public string GameName { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string Developer { get; set; }

        [MaxLength(10)]
        public string ReleaseYear { get; set; }

        [ForeignKey("GameGenre")]
        public string GameGenreId { get; set; }

        public GameGenre GameGenre { get; set; }

        public ICollection<Achievements> Achievements { get; set; }
    }
}
