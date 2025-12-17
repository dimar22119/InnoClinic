using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfilesService.Api.Extensions;
using ProfilesService.Application.Dtos.Patients;
using ProfilesService.Application.Interfaces.Services;

namespace ProfilesService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Patient,Admin")]
    public class PatientsController(IPatientService patientService) : ControllerBase
    {
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PatientResponse>> GetById(Guid id)
        {
            var result = await patientService.GetByIdAsync(id);
            return result.ToActionResult(this);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientResponse>>> GetAll()
        {
            var result = await patientService.GetAllAsync();
            return result.ToActionResult(this);
        }

        [HttpPost]
        public async Task<ActionResult<PatientResponse>> Create([FromBody] CreatePatientRequest request)
        {
            var result = await patientService.CreateAsync(request);
            if (!result.IsSuccess)
                return result.ToActionResult(this);

            return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<PatientResponse>> Update(Guid id, [FromBody] UpdatePatientRequest request)
        {
            if (id != request.Id) return BadRequest("Id mismatch");

            var result = await patientService.UpdateAsync(request);
            return result.ToActionResult(this);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await patientService.DeleteAsync(id);
            return result.ToActionResult(this);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<PatientResponse>>> Search([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var result = await patientService.GetByNameAsync(firstName, lastName);
            return result.ToActionResult(this);
        }
    }
}
