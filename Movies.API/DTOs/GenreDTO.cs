using System.ComponentModel.DataAnnotations;

namespace Movies.API.DTOs
{
    public class GenreDTO
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
