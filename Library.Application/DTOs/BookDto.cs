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
        public bool? IsAvailable { get; set; }

        public ICollection<AuthorDto> Authors { get; set; } = new List<AuthorDto>();
        public ICollection<GenreDto> Genres { get; set; } = new List<GenreDto>();
   
        public ICollection<RentalDto> Rentals { get; set; } = new List<RentalDto>();
        public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();
        public ICollection<RatingDto> Ratings { get; set; } = new List<RatingDto>();
        public double? AverageRating { get; set; }
    }

    public class CreateBookDto
    {
        public string? Title { get; set; }
        public IFormFile? Image { get; set; } 
        public DateTime? PublicationYear { get; set; }
        public bool? IsAvailable { get; set; } = true;
        public List<int>? AuthorIds { get; set; }
        public List<int>? GenreIds { get; set; }

    }




}
