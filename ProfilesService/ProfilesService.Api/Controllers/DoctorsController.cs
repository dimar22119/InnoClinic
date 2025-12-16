using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfilesService.Api.Extensions;
using ProfilesService.Application.Dtos.Doctors;
using ProfilesService.Application.Interfaces.Services;

namespace ProfilesService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Doctor,Admin")]
    public class DoctorsController(IDoctorService doctorService) : ControllerBase
    {
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DoctorResponse>> GetById(Guid id)
        {
            var result = await doctorService.GetByIdAsync(id);
            return result.ToActionResult(this);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorResponse>>> GetAll()
        {
            var result = await doctorService.GetAllAsync();
            return result.ToActionResult(this);
        }

        [HttpPost]
        public async Task<ActionResult<DoctorResponse>> Create([FromBody] CreateDoctorRequest request)
        {
            var result = await doctorService.CreateAsync(request);
            if (!result.IsSuccess)
                return result.ToActionResult(this);

            return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<DoctorResponse>> Update(Guid id, [FromBody] UpdateDoctorRequest request)
        {
            if (id != request.Id) return BadRequest("Id mismatch");

            var result = await doctorService.UpdateAsync(request);
            return result.ToActionResult(this);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await doctorService.DeleteAsync(id);
            return result.ToActionResult(this);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<DoctorResponse>>> SearchByName([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var result = await doctorService.GetByNameAsync(firstName, lastName);
            return result.ToActionResult(this);
        }
    }
}
