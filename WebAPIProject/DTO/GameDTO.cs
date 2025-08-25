using System.ComponentModel.DataAnnotations;
using WebAPIProject.Models;

namespace WebAPIProject.DTO
{
    public class GameDTO
    {
        public string GameName { get; set; }

      
        public string Description { get; set; }

       
        public string Developer { get; set; }

        public string ReleaseYear { get; set; }

        public string GameGenreId { get; set; }

    }
}
