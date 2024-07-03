
using System;
using System.Collections.Generic;

namespace Library.Application.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public DateTime? PublicationYear { get; set; }
        public bool IsAvailable { get; set; }

        public ICollection<AuthorDto> Authors { get; set; } = new List<AuthorDto>();    
        public ICollection<GenreDto> Genres { get; set; } = new List<GenreDto>();
        public ICollection<RentalDto> Rentals { get; set; } = new List<RentalDto>();
    }

    public class CreateBookDto
    {
        public string Title { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public DateTime? PublicationYear { get; set; }  
        public bool IsAvailable { get; set; }
        public List<int> AuthorIds { get; set; }
        public List<int> GenreIds { get; set; }
    }
}
