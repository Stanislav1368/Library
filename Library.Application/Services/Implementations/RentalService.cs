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
    public class RentalService : IRentalService
    {
        private readonly LibraryContext _context;

        public RentalService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RentalDto>> GetAllRentalsAsync()
        {
            return await _context.Rentals
                .Include(r => r.Book).ThenInclude(b => b.AuthorBooks).ThenInclude(ab => ab.Author)
                .Include(r => r.Book).ThenInclude(b => b.GenreBooks).ThenInclude(gb => gb.Genre)
                .Include(r => r.Renter)
                .Include(r => r.Status)
                .Select(r => new RentalDto
                {
                    Id = r.Id,
                    RentedAt = r.RentedAt,
                    ReturnedAt = r.ReturnedAt,
                    ActualReturnedAt = r.ActualReturnedAt,
                    Review = r.Review,
                    Book = new BookDto
                    {
                        Id = r.Book.Id,
                        Title = r.Book.Title,
                        PublicationYear = r.Book.PublicationYear,
                        IsAvailable = r.Book.IsAvailable,
                        Authors = r.Book.AuthorBooks.Select(ab => new AuthorDto
                        {
                            Id = ab.Author.Id,
                            FirstName = ab.Author.FirstName,
                            LastName = ab.Author.LastName,
                            Patronymic = ab.Author.Patronymic
                        }).ToList(),
                        Genres = r.Book.GenreBooks.Select(gb => new GenreDto
                        {
                            Id = gb.Genre.Id,
                            Name = gb.Genre.Name
                        }).ToList()
                    },
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
                    },
                    Librarian = new LibrarianDto
                    {
                        Id = r.Librarian.Id,
                        FirstName = r.Librarian.FirstName,
                        LastName = r.Librarian.LastName,
                        Patronymic = r.Librarian.Patronymic
                    }
                })
                .ToListAsync();
        }

        public async Task<RentalDto> GetRentalByIdAsync(int id)
        {
            var rental = await _context.Rentals
                .Include(r => r.Book).ThenInclude(b => b.AuthorBooks).ThenInclude(ab => ab.Author)
                .Include(r => r.Book).ThenInclude(b => b.GenreBooks).ThenInclude(gb => gb.Genre)
                .Include(r => r.Renter)
                .Include(r => r.Status)
                .SingleOrDefaultAsync(r => r.Id == id);

            if (rental == null)
                return null;

            return new RentalDto
            {
                Id = rental.Id,
                RentedAt = rental.RentedAt,
                ReturnedAt = rental.ReturnedAt,
                Review = rental.Review,
                Book = new BookDto
                {
                    Id = rental.Book.Id,
                    Title = rental.Book.Title,
                    PublicationYear = rental.Book.PublicationYear,
                    IsAvailable = rental.Book.IsAvailable,
                    Authors = rental.Book.AuthorBooks.Select(ab => new AuthorDto
                    {
                        Id = ab.Author.Id,
                        FirstName = ab.Author.FirstName,
                        LastName = ab.Author.LastName,
                        Patronymic = ab.Author.Patronymic
                    }).ToList(),
                    Genres = rental.Book.GenreBooks.Select(gb => new GenreDto
                    {
                        Id = gb.Genre.Id,
                        Name = gb.Genre.Name
                    }).ToList()
                },
                Renter = new RenterDto
                {
                    Id = rental.Renter.Id,
                    FirstName = rental.Renter.FirstName,
                    LastName = rental.Renter.LastName,
                    Patronymic = rental.Renter.Patronymic,
                    Address = rental.Renter.Address,
                    ContactNumber = rental.Renter.ContactNumber
                },
                Status = new StatusDto
                {
                    Id = rental.Status.Id,
                    Name = rental.Status.Name
                }
            };
        }

        public async Task<RentalDto> CreateRentalAsync(CreateRentalDto createRentalDto)
        {
            var rental = new Rental
            {
                BookId = createRentalDto.BookId,
                RenterId = createRentalDto.RenterId,
                LibrarianId = createRentalDto.LibrarianId,
                StatusId = createRentalDto.StatusId,
                RentedAt = createRentalDto.RentedAt,
                ReturnedAt = createRentalDto.ReturnedAt,
            };

            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            // Load the rental with related entities
            rental = await _context.Rentals
                .Include(r => r.Book)
                    .ThenInclude(b => b.AuthorBooks)
                        .ThenInclude(ab => ab.Author)
                .Include(r => r.Book)
                    .ThenInclude(b => b.GenreBooks)
                        .ThenInclude(gb => gb.Genre)
                .Include(r => r.Renter)
                .Include(r => r.Status)
                .Include(r => r.Librarian)
                .FirstOrDefaultAsync(r => r.Id == rental.Id);

            return new RentalDto
            {
                Id = rental.Id,
                RentedAt = rental.RentedAt,
                ReturnedAt = rental.ReturnedAt,
                Review = rental.Review,
                Book = new BookDto
                {
                    Id = rental.BookId,
                    Title = rental.Book.Title,
                    PublicationYear = rental.Book.PublicationYear,
                    IsAvailable = rental.Book.IsAvailable,
                    Authors = rental.Book.AuthorBooks.Select(ab => new AuthorDto
                    {
                        Id = ab.Author.Id,
                        FirstName = ab.Author.FirstName,
                        LastName = ab.Author.LastName,
                        Patronymic = ab.Author.Patronymic
                    }).ToList(),
                    Genres = rental.Book.GenreBooks.Select(gb => new GenreDto
                    {
                        Id = gb.Genre.Id,
                        Name = gb.Genre.Name
                    }).ToList()
                },
                Renter = new RenterDto
                {
                    Id = rental.Renter.Id,
                    FirstName = rental.Renter.FirstName,
                    LastName = rental.Renter.LastName,
                    Patronymic = rental.Renter.Patronymic,
                    Address = rental.Renter.Address,
                    ContactNumber = rental.Renter.ContactNumber
                },
                Status = new StatusDto
                {
                    Id = rental.Status.Id,
                    Name = rental.Status.Name
                },
                Librarian = new LibrarianDto
                {
                    Id = rental.Librarian.Id,
                    FirstName = rental.Librarian.FirstName,
                    LastName = rental.Librarian.LastName,
                    Patronymic = rental.Librarian.Patronymic
                }
            };
        }


        public async Task<RentalDto> UpdateRentalAsync(int id, UpdateRentalDto updateRentalDto)
        {
            var rental = await _context.Rentals.FindAsync(id);

            if (rental == null)
                throw new KeyNotFoundException();

            rental.RenterId = updateRentalDto.RenterId ?? rental.RenterId;
            rental.StatusId = updateRentalDto.StatusId ?? rental.StatusId;
            rental.RentedAt = updateRentalDto.RentedAt ?? rental.RentedAt;
            rental.ReturnedAt = updateRentalDto.ReturnedAt ?? rental.ReturnedAt;
            rental.ActualReturnedAt = updateRentalDto.ActualReturnedAt ?? rental.ActualReturnedAt;
            rental.Review = updateRentalDto.Review ?? rental.Review;

            await _context.SaveChangesAsync();

            // Load the updated rental with related entities
            rental = await _context.Rentals
                .Include(r => r.Book)
                    .ThenInclude(b => b.AuthorBooks)
                        .ThenInclude(ab => ab.Author)
                .Include(r => r.Book)
                    .ThenInclude(b => b.GenreBooks)
                        .ThenInclude(gb => gb.Genre)
                .Include(r => r.Renter)
                .Include(r => r.Status)
                .Include(r => r.Librarian)
                .FirstOrDefaultAsync(r => r.Id == rental.Id);

            return new RentalDto
            {
                Id = rental.Id,
                RentedAt = rental.RentedAt,
                ReturnedAt = rental.ReturnedAt,
                ActualReturnedAt = rental.ActualReturnedAt,
                Review = rental.Review,
                Book = new BookDto
                {
                    Id = rental.BookId,
                    Title = rental.Book.Title,
                    PublicationYear = rental.Book.PublicationYear,
                    IsAvailable = rental.Book.IsAvailable,
                    Authors = rental.Book.AuthorBooks.Select(ab => new AuthorDto
                    {
                        Id = ab.Author.Id,
                        FirstName = ab.Author.FirstName,
                        LastName = ab.Author.LastName,
                        Patronymic = ab.Author.Patronymic
                    }).ToList(),
                    Genres = rental.Book.GenreBooks.Select(gb => new GenreDto
                    {
                        Id = gb.Genre.Id,
                        Name = gb.Genre.Name
                    }).ToList()
                },
                Renter = new RenterDto
                {
                    Id = rental.Renter.Id,
                    FirstName = rental.Renter.FirstName,
                    LastName = rental.Renter.LastName,
                    Patronymic = rental.Renter.Patronymic,
                    Address = rental.Renter.Address,
                    ContactNumber = rental.Renter.ContactNumber
                },
                Status = new StatusDto
                {
                    Id = rental.Status.Id,
                    Name = rental.Status.Name
                },
                Librarian = new LibrarianDto
                {
                    Id = rental.Librarian.Id,
                    FirstName = rental.Librarian.FirstName,
                    LastName = rental.Librarian.LastName,
                    Patronymic = rental.Librarian.Patronymic
                }
            };
        }

        public async Task DeleteRentalAsync(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);

            if (rental == null)
                throw new KeyNotFoundException();

            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();
        }
    }
}
