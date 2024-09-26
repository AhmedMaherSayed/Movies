using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Models
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public string StoreLine { get; set; }
        //byte[] to add images in the Database
        public string Poster { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
