using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIProject.Models
{
    public class Achievements
    {
        [Key]
        [MaxLength(20)]
        public string AchievementId { get; set; }

        [Required, MaxLength(500)]
        public string AchievementName { get; set; }

        [MaxLength(50)]
        public string Badge { get; set; }

        [MaxLength(50)]
        public string Difficulty { get; set; }

        [MaxLength(100)]
        public string Reward { get; set; }

        [ForeignKey("Game")]
        public string GameId { get; set; }

        public Game Game { get; set; }
    }
}
