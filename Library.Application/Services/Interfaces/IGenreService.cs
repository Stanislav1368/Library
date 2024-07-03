using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Application.DTOs;

namespace Library.Application.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetAllGenresAsync();
        //Task<GenreDto> GetGenresByIdAsync(int id);
        Task<GenreDto> CreateGenreAsync(GenreDto genreDto);
        //Task UpdateGenresAsync(int id, GenreDto genreDto);
        //Task DeleteGenresAsync(int id);
    }
}
