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
        Task CommentOnTheBookAsync(int id, CreateCommentDto createCommentDto);
        Task AddRatingAsync(int id, AddRatingDto addRatingDto);
        Task DeleteBookAsync(int id);
    }
}
