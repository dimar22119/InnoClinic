using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.Models;
using CatalogService.Application.Models.Specializations;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecializationsController(ISpecializationManager manager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var paginationParams = new PaginationParams(pageNumber, pageSize);

            var result = await manager.GetPagedAsync(paginationParams);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
            => Ok(await manager.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateSpecializationDto dto, CancellationToken cancellationToken)
        {
            var result = await manager.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateSpecializationDto dto, CancellationToken cancellationToken)
        {
            await manager.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await manager.DeleteAsync(id);
            return NoContent();
        }
    }

}
