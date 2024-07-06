using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Application.DTOs;

namespace Library.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LibrarianDto> AuthenticateAsync(string username, string password);
        Task<RenterDto> AuthenticateAsync(string username, string password, bool isRenter);
    }

}
