using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Application.DTOs;

namespace Library.Application.Services.Interfaces
{
    public interface IStatusService
    {
        Task<IEnumerable<StatusDto>> GetAllStatusesAsync();
        //Task<StatusDto> GetStatusByIdAsync(int id);
        Task<StatusDto> CreateStatusAsync(StatusDto statusDto);
        //Task UpdateStatusAsync(int id, StatusDto statusDto);
        //Task DeleteStatusAsync(int id);
    }
}
