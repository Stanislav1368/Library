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
    public class AuthorService : IAuthorService
    {
        private readonly LibraryContext _context;

        public AuthorService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            return await _context.Authors
                .Select(a => new AuthorDto
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Patronymic = a.Patronymic
                })
                .ToListAsync();
        }

        public async Task<AuthorDto> CreateAuthorAsync(AuthorDto authorDto)
        {
            var author = new Author
            {
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName,
                Patronymic = authorDto.Patronymic
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            authorDto.Id = author.Id;
            return authorDto;
        }
    }
}
