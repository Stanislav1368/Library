using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Application.DTOs;
using Library.Application.Services.Interfaces;
using Library.Domain.Entities;
using Library.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Services.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly LibraryContext _context;

        public GenreService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GenreDto>> GetAllGenresAsync()
        {
            return await _context.Genres
                .Select(g => new GenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .ToListAsync();
        }

        public async Task<GenreDto> CreateGenreAsync(GenreDto genreDto)
        {
            var genre = new Genre
            {
                Name = genreDto.Name
            };

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            genreDto.Id = genre.Id;
            return genreDto;
        }
    }
}
