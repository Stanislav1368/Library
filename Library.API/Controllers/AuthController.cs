﻿using Library.Application.DTOs;
using Library.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<object>> Login(AuthRequestDto authRequestDto)
        {
            if (authRequestDto.IsRenter)
            {
                var renter = await _authService.AuthenticateAsync(authRequestDto.Login, authRequestDto.Password, true);

                if (renter == null)
                    return Unauthorized();

                return Ok(renter);
            }
            else
            {
                var librarian = await _authService.AuthenticateAsync(authRequestDto.Login, authRequestDto.Password);

                if (librarian == null)
                    return Unauthorized();

                return Ok(librarian);
            }
        }
    }
}
