using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController(IServiceManager manager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
            => Ok(await manager.GetAllAsync());

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
            => Ok(await manager.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateServiceDto dto, CancellationToken cancellationToken)
        {
            var result = await manager.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateServiceDto dto, CancellationToken cancellationToken)
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
