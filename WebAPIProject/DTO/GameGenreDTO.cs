using System.ComponentModel.DataAnnotations;

namespace WebAPIProject.DTO
{
    public class GameGenreDTO
    {
        public string GenreName { get; set; }
        public string Description { get; set; }
        public string Popularity { get; set; }
    }
}
