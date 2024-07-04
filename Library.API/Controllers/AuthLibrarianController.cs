using Library.Application.DTOs;
using Library.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class AuthLibrarianController : ControllerBase
    {
        private readonly IAuthLibrarianService _authService;
        public AuthLibrarianController(IAuthLibrarianService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<LibrarianDto>> Login(AuthLibrarianDto authLibrarianDto)
        {
            var librarian = await _authService.AuthenticateAsync(authLibrarianDto.Login, authLibrarianDto.Password);

            if (librarian == null)
                return Unauthorized();

            return Ok(librarian);
        }
    }
  
}
