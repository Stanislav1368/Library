
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library.Application.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public DateTime? PublicationYear { get; set; }
        public bool IsAvailable { get; set; }

        public ICollection<AuthorDto> Authors { get; set; }    
        public ICollection<GenreDto> Genres { get; set; }
        [JsonIgnore]
        public ICollection<RentalDto> Rentals { get; set; } 
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
