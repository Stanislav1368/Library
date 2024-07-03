using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Application.DTOs;
using Library.Application.Services.Interfaces;
using Library.Domain.Entities;
using Library.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Services.Implementations
{
    public class StatusService : IStatusService
    {
        private readonly LibraryContext _context;

        public StatusService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StatusDto>> GetAllStatusesAsync()
        {
            return await _context.Statuses
                .Select(s => new StatusDto
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToListAsync();
        }

        public async Task<StatusDto> CreateStatusAsync(StatusDto statusDto)
        {
            var status = new Status
            {
                Name = statusDto.Name
            };

            _context.Statuses.Add(status);
            await _context.SaveChangesAsync();

            statusDto.Id = status.Id;
            return statusDto;
        }
    }
}
