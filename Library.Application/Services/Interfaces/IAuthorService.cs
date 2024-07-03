using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Application.DTOs;

namespace Library.Application.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
        //Task<AuthorDto> GetAuthorByIdAsync(int id);
        Task<AuthorDto> CreateAuthorAsync(AuthorDto authorDto);
        //Task UpdateAuthorsync(int id, AuthorDto authorDto);
        //Task DeleteAuthorAsync(int id);
    }
}
