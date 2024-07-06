using System.Linq;
using System.Threading.Tasks;
using Library.Application.DTOs;
using Library.Application.Services.Interfaces;
using Library.Infrastructure;

public class AuthService : IAuthService
{
    private readonly LibraryContext _context;

    public AuthService(LibraryContext context)
    {
        _context = context;
    }

    public async Task<LibrarianDto> AuthenticateAsync(string login, string password)
    {
        var librarian = _context.Librarians.FirstOrDefault(l => l.Login == login && l.Password == password);

        if (librarian == null)
        {
            return null;
        }
        return new LibrarianDto
        {
            Id = librarian.Id,
            FirstName = librarian.FirstName,
            LastName = librarian.LastName,
            Patronymic = librarian.Patronymic
        };
    }

    public async Task<RenterDto> AuthenticateAsync(string login, string password, bool isRenter)
    {
        if (!isRenter)
        {
            return null;
        }

        var renter = _context.Renters.FirstOrDefault(r => r.ContactNumber == login && r.Password == password);

        if (renter == null)
        {
            return null;
        }
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
}
