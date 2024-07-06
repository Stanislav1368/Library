using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Application.DTOs;
using Library.Application.Services.Interfaces;
using Library.Domain.Entities;
using Library.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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
                    }).ToList(),
                    Comments = b.Comments.Select(c => new CommentDto
                    {
                        CommentText = c.CommentText,
                        CommentedAt = c.CommentedAt,
                    }).ToList(),
                    AverageRating = b.Ratings.Count > 0 ? b.Ratings.Average(r => r.RatingValue) : 0.0,
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
                    : null,
                IsAvailable = createBookDto.IsAvailable
            };

            if (createBookDto.Image != null)
            {
                var fileExtension = Path.GetExtension(createBookDto.Image.FileName);
                var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine("wwwroot", "images", uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await createBookDto.Image.CopyToAsync(stream);
                }

                book.ImagePath = $"/images/{uniqueFileName}";
            }

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
                ImagePath = book.ImagePath,
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

        public async Task CommentOnTheBookAsync(int id, CreateCommentDto createCommentDto)
        {
            var comment = new Comment
            {
                RenterId = createCommentDto.RenterId,
                BookId = id,
                CommentText = createCommentDto.CommentText,
                CommentedAt = DateTime.UtcNow 
            };

            

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }
        public async Task AddRatingAsync(int id, AddRatingDto addRatingDto)
        {
            var existingRating = await _context.Ratings.FirstOrDefaultAsync(r => r.RenterId == addRatingDto.RenterId && r.BookId == id);

            if (existingRating != null)
            {
                existingRating.RatingValue = addRatingDto.RatingValue;
            }
            else
            {
                var rating = new Rating
                {
                    RenterId = addRatingDto.RenterId,
                    BookId = id,
                    RatingValue = addRatingDto.RatingValue
                };

                _context.Ratings.Add(rating);
            }

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
