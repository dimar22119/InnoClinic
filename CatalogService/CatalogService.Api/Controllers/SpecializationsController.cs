using CatalogService.Application.Dtos.Specializations;
using CatalogService.Application.Interfaces.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecializationsController(ISpecializationManager manager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await manager.GetAllAsync();
            return Ok(items.Adapt<IEnumerable<SpecializationResponseDto>>());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await manager.GetByIdAsync(id);
            if (item is null) return NotFound();

            return Ok(item.Adapt<SpecializationResponseDto>());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSpecializationDto dto)
        {
            var result = await manager.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result.Adapt<SpecializationResponseDto>());
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateSpecializationDto dto)
        {
            await manager.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await manager.DeleteAsync(id);
            return NoContent();
        }
    }

}
