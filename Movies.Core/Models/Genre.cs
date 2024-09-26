namespace Movies.Core.Models
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
    }
}