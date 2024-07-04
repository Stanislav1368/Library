using Library.Application.DTOs;
using Library.Application.Services.Interfaces;
using Library.Infrastructure;

public class AuthLibrarianService : IAuthLibrarianService
{
    private readonly LibraryContext _context;

    public AuthLibrarianService(LibraryContext context)
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
}