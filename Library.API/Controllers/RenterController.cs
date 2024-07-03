using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Application.DTOs;
using Library.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentersController : ControllerBase
    {
        private readonly IRenterService _renterService;

        public RentersController(IRenterService renterService)
        {
            _renterService = renterService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RenterDto>>> GetAllRenters()
        {
            var renters = await _renterService.GetAllRentersAsync();
            return Ok(renters);
        }

        [HttpPost]
        public async Task<ActionResult<RenterDto>> CreateRenter(RenterDto renterDto)
        {
            var renter = await _renterService.CreateRenterAsync(renterDto);
            return CreatedAtAction(nameof(GetAllRenters), new { id = renter.Id }, renter);
        }
    }
}
