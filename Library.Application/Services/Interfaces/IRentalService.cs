using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Application.DTOs;

namespace Library.Application.Services.Interfaces
{
    public interface IRentalService
    {
        Task<IEnumerable<RentalDto>> GetAllRentalsAsync();
        Task<RentalDto> GetRentalByIdAsync(int id);
        Task<RentalDto> CreateRentalAsync(CreateRentalDto createRentalDto);
        Task UpdateRentalAsync(int id, UpdateRentalDto updateRentalDto);
        Task DeleteRentalAsync(int id);
    }
}
