using Microsoft.AspNetCore.Http;
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
        public ICollection<CommentDto> Comments { get; set; }
        public double? AverageRating { get; set; }
    }

    public class CreateBookDto
    {
        public string Title { get; set; }
        public IFormFile Image { get; set; } // Add this property
        public DateTime? PublicationYear { get; set; }
        public bool IsAvailable { get; set; } = true;
        public List<int>? AuthorIds { get; set; }
        public List<int>? GenreIds { get; set; }

    }




}
