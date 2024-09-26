using System.ComponentModel.DataAnnotations;

namespace Movies.API.DTOs
{
    public class MovieWithGenreIdDTO
    {
        [MaxLength(250)]
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500)]
        public string StoreLine { get; set; }
        public string Poster { get; set; }
        public int GenreId { get; set; }
    }
}
