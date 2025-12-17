using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfilesService.Api.Extensions;
using ProfilesService.Application.Dtos.Receptionist;
using ProfilesService.Application.Interfaces.Services;

namespace ProfilesService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] 
    public class ReceptionistsController(IReceptionistService receptionistService) : ControllerBase
    {
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ReceptionistResponse>> GetById(Guid id)
        {
            var result = await receptionistService.GetByIdAsync(id);
            return result.ToActionResult(this);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceptionistResponse>>> GetAll()
        {
            var result = await receptionistService.GetAllAsync();
            return result.ToActionResult(this);
        }

        [HttpPost]
        public async Task<ActionResult<ReceptionistResponse>> Create([FromBody] CreateReceptionistRequest request)
        {
            var result = await receptionistService.CreateAsync(request);
            if (!result.IsSuccess)
                return result.ToActionResult(this);

            return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ReceptionistResponse>> Update(Guid id, [FromBody] UpdateReceptionistRequest request)
        {
            if (id != request.Id) return BadRequest("Id mismatch");

            var result = await receptionistService.UpdateAsync(request);
            return result.ToActionResult(this);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await receptionistService.DeleteAsync(id);
            return result.ToActionResult(this);
        }
    }
}
