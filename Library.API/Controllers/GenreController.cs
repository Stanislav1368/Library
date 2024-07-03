using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Application.DTOs;
using Library.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetAllGenres()
        {
            var genres = await _genreService.GetAllGenresAsync();
            return Ok(genres);
        }

        [HttpPost]
        public async Task<ActionResult<GenreDto>> CreateGenre(GenreDto genreDto)
        {
            var genre = await _genreService.CreateGenreAsync(genreDto);
            return CreatedAtAction(nameof(GetAllGenres), new { id = genre.Id }, genre);
        }
    }
}
