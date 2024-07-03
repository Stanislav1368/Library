using System.Collections.Generic;
using System.Threading.Tasks;

using Library.Application.DTOs;

namespace Library.Application.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto> GetBookByIdAsync(int id);
        Task<BookDto> CreateBookAsync(CreateBookDto createBookDto);
        Task UpdateBookAsync(int id, BookDto bookDto);
        Task DeleteBookAsync(int id);
    }
}
