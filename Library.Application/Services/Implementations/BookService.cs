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
    public class BookService : IBookService
    {
        private readonly LibraryContext _context;

        public BookService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            return await _context.Books
                .Include(b => b.AuthorBooks).ThenInclude(ab => ab.Author)
                .Include(b => b.GenreBooks).ThenInclude(gb => gb.Genre)
                .Include(b => b.Rentals)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    PublicationYear = b.PublicationYear,
                    ImagePath = b.ImagePath,
                    IsAvailable = b.IsAvailable,
                    Authors = b.AuthorBooks.Select(ab => new AuthorDto
                    {
                        Id = ab.Author.Id,
                        FirstName = ab.Author.FirstName,
                        LastName = ab.Author.LastName,
                        Patronymic = ab.Author.Patronymic
                    }).ToList(),
                    Genres = b.GenreBooks.Select(gb => new GenreDto
                    {
                        Id = gb.Genre.Id,
                        Name = gb.Genre.Name
                    }).ToList(),
                    Rentals = b.Rentals.Select(r => new RentalDto
                    {
                        Id = r.Id,
                        RentedAt = r.RentedAt,
                        ReturnedAt = r.ReturnedAt,
                        Review = r.Review,
                        Renter = new RenterDto
                        {
                            Id = r.Renter.Id,
                            FirstName = r.Renter.FirstName,
                            LastName = r.Renter.LastName,
                            Patronymic = r.Renter.Patronymic,
                            Address = r.Renter.Address,
                            ContactNumber = r.Renter.ContactNumber
                        },
                        Status = new StatusDto
                        {
                            Id = r.Status.Id,
                            Name = r.Status.Name
                        }
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _context.Books
                .Include(b => b.AuthorBooks).ThenInclude(ab => ab.Author)
                .Include(b => b.GenreBooks).ThenInclude(gb => gb.Genre)
                .Include(b => b.Rentals)
                .SingleOrDefaultAsync(b => b.Id == id);

            if (book == null) {
                return null;
            }
                

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                PublicationYear = book.PublicationYear,
                IsAvailable = book.IsAvailable,
                Authors = book.AuthorBooks.Select(ab => new AuthorDto
                {
                    Id = ab.Author.Id,
                    FirstName = ab.Author.FirstName,
                    LastName = ab.Author.LastName,
                    Patronymic = ab.Author.Patronymic
                }).ToList(),
                Genres = book.GenreBooks.Select(gb => new GenreDto
                {
                    Id = gb.Genre.Id,
                    Name = gb.Genre.Name
                }).ToList(),
                Rentals = book.Rentals.Select(r => new RentalDto
                {
                    Id = r.Id,
                    RentedAt = r.RentedAt,
                    ReturnedAt = r.ReturnedAt,
                    Review = r.Review,
                    Renter = new RenterDto
                    {
                        Id = r.Renter.Id,
                        FirstName = r.Renter.FirstName,
                        LastName = r.Renter.LastName,
                        Patronymic = r.Renter.Patronymic,
                        Address = r.Renter.Address,
                        ContactNumber = r.Renter.ContactNumber
                    },
                    Status = new StatusDto
                    {
                        Id = r.Status.Id,
                        Name = r.Status.Name
                    }
                }).ToList()
            };
        }

        public async Task<BookDto> CreateBookAsync(CreateBookDto createBookDto)
        {
            var book = new Book
            {
                Title = createBookDto.Title,
                PublicationYear = createBookDto.PublicationYear.HasValue
                       ? createBookDto.PublicationYear.Value.ToUniversalTime()
                       : (DateTime?)null,
                IsAvailable = createBookDto.IsAvailable
            };

            // Add authors to the book
            if (createBookDto.AuthorIds != null && createBookDto.AuthorIds.Any())
            {
                foreach (var authorId in createBookDto.AuthorIds)
                {
                    var author = await _context.Authors.FindAsync(authorId);
                    if (author != null)
                    {
                        book.AuthorBooks.Add(new AuthorBook { Author = author, Book = book });
                    }
                }
            }

            // Add genres to the book
            if (createBookDto.GenreIds != null && createBookDto.GenreIds.Any())
            {
                foreach (var genreId in createBookDto.GenreIds)
                {
                    var genre = await _context.Genres.FindAsync(genreId);
                    if (genre != null)
                    {
                        book.GenreBooks.Add(new GenreBook { Genre = genre, Book = book });
                    }
                }
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                PublicationYear = book.PublicationYear,
                IsAvailable = book.IsAvailable,
                Authors = book.AuthorBooks.Select(ab => new AuthorDto
                {
                    Id = ab.Author.Id,
                    FirstName = ab.Author.FirstName,
                    LastName = ab.Author.LastName,
                    Patronymic = ab.Author.Patronymic
                }).ToList(),
                Genres = book.GenreBooks.Select(gb => new GenreDto
                {
                    Id = gb.Genre.Id,
                    Name = gb.Genre.Name
                }).ToList(),
            };
        }

        public async Task UpdateBookAsync(int id, BookDto bookDto)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                throw new KeyNotFoundException();

            book.Title = bookDto.Title;
            book.PublicationYear = bookDto.PublicationYear;
            book.IsAvailable = bookDto.IsAvailable;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                throw new KeyNotFoundException();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
