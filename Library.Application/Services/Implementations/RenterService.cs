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
    public class RenterService : IRenterService
    {
        private readonly LibraryContext _context;

        public RenterService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RenterDto>> GetAllRentersAsync()
        {
            return await _context.Renters
                .Include(r => r.Rentals)
                .Select(r => new RenterDto
                {
                    Id = r.Id,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    Patronymic = r.Patronymic,
                    Address = r.Address,
                    ContactNumber = r.ContactNumber,
                    Rentals = r.Rentals.Select(rental => new RentalDto
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
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<RenterDto> GetRenterByIdAsync(int id)
        {
            var renter = await _context.Renters
                .Include(r => r.Rentals)
                .SingleOrDefaultAsync(r => r.Id == id);

            if (renter == null)
                return null;

            return new RenterDto
            {
                Id = renter.Id,
                FirstName = renter.FirstName,
                LastName = renter.LastName,
                Patronymic = renter.Patronymic,
                Address = renter.Address,
                ContactNumber = renter.ContactNumber,
                Rentals = renter.Rentals.Select(rental => new RentalDto
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
                }).ToList()
            };
        }

        public async Task<RenterDto> CreateRenterAsync(RenterDto renterDto)
        {
            var renter = new Renter
            {
                FirstName = renterDto.FirstName,
                LastName = renterDto.LastName,
                Patronymic = renterDto.Patronymic,
                Address = renterDto.Address,
                ContactNumber = renterDto.ContactNumber
            };

            _context.Renters.Add(renter);
            await _context.SaveChangesAsync();

            return new RenterDto
            {
                Id = renter.Id,
                FirstName = renter.FirstName,
                LastName = renter.LastName,
                Patronymic = renter.Patronymic,
                Address = renter.Address,
                ContactNumber = renter.ContactNumber
            };
        }

        public async Task UpdateRenterAsync(int id, RenterDto renterDto)
        {
            var renter = await _context.Renters.FindAsync(id);

            if (renter == null)
                throw new KeyNotFoundException();

            renter.FirstName = renterDto.FirstName;
            renter.LastName = renterDto.LastName;
            renter.Patronymic = renterDto.Patronymic;
            renter.Address = renterDto.Address;
            renter.ContactNumber = renterDto.ContactNumber;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteRenterAsync(int id)
        {
            var renter = await _context.Renters.FindAsync(id);

            if (renter == null)
                throw new KeyNotFoundException();

            _context.Renters.Remove(renter);
            await _context.SaveChangesAsync();
        }
    }
}
