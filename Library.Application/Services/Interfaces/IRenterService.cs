using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Application.DTOs;

namespace Library.Application.Services.Interfaces
{
    public interface IRenterService
    {
        Task<IEnumerable<RenterDto>> GetAllRentersAsync();
        //Task<RenterDto> GetRenterByIdAsync(int id);
        Task<RenterDto> CreateRenterAsync(CreateRenterDto createRenterDto);
        //Task UpdateRenterAsync(int id, RenterDto renterDto);
        //Task DeleteRenterAsync(int id);
    }
}
