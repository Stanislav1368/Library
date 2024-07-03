using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public DateTime? PublicationYear { get; set; }
        public bool IsAvailable { get; set; } = true;
        public ICollection<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();
        public ICollection<GenreBook> GenreBooks { get; set; } = new List<GenreBook>();
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
