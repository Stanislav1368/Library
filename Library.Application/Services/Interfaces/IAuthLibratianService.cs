using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Application.DTOs;

namespace Library.Application.Services.Interfaces
{
    public interface IAuthLibrarianService
    {
        Task<LibrarianDto> AuthenticateAsync(string username, string password);
    }

}
