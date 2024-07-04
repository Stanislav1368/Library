using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Application.DTOs;
using Library.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalDto>>> GetRentals()
        {
            var rentals = await _rentalService.GetAllRentalsAsync();
            return Ok(rentals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RentalDto>> GetRental(int id)
        {
            var rental = await _rentalService.GetRentalByIdAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            return Ok(rental);
        }

        [HttpPost]
        public async Task<ActionResult<RentalDto>> CreateRental(CreateRentalDto createRentalDto)
        {
            var createdRental = await _rentalService.CreateRentalAsync(createRentalDto);
            return CreatedAtAction(nameof(GetRental), new { id = createdRental.Id }, createdRental);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRental(int id, UpdateRentalDto updateRentalDto)
        {
            try
            {
                await _rentalService.UpdateRentalAsync(id, updateRentalDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental(int id)
        {
            try
            {
                await _rentalService.DeleteRentalAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
