using Inventory.Application.Dtos.InventoryQuarantineDtos;
using Inventory.Application.Features.InventoryQuarantineFeatures.Commands.CreateInventoryQuarantine;
using Inventory.Application.Features.InventoryQuarantineFeatures.Commands.DeleteInventoryQuarantine;
using Inventory.Application.Features.InventoryQuarantineFeatures.Commands.UpdateInventoryQuarantine;
using Inventory.Application.Features.InventoryQuarantineFeatures.Commands.UpdateInventoryQuarantineStatus;
using Inventory.Application.Features.InventoryQuarantineFeatures.Queries.GetAllInventoryQuarantines;
using Inventory.Application.Features.InventoryQuarantineFeatures.Queries.GetInventoryQuarantineById;
using Inventory.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryQuarantineController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InventoryQuarantineController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid? productId, [FromQuery] Guid? locationId, [FromQuery] QuarantineStatus? status)
        {
            var response = await _mediator.Send(new GetAllInventoryQuarantinesQueryRequest
            {
                ProductId = productId,
                LocationId = locationId,
                Status = status
            });
            return Ok(response.InventoryQuarantines);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetInventoryQuarantineByIdQueryRequest { Id = id });
            if (response.InventoryQuarantine == null) return NotFound();
            return Ok(response.InventoryQuarantine);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInventoryQuarantineDto dto)
        {
            var response = await _mediator.Send(new CreateInventoryQuarantineCommandRequest { InventoryQuarantine = dto });
            if (!response.Success) return BadRequest();
            return Ok(response.InventoryQuarantine);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateInventoryQuarantineDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var response = await _mediator.Send(new UpdateInventoryQuarantineCommandRequest { InventoryQuarantine = dto });
            if (!response.Success) return NotFound();
            return Ok(response.InventoryQuarantine);
        }


  
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid InventoryQuarantineId, QuarantineStatus NewStatus)
        {
          
            var response = await _mediator.Send(new UpdateInventoryQuarantineStatusCommandRequest
            {
                InventoryQuarantineId = InventoryQuarantineId,
                NewStatus = NewStatus
            });
            if (!response.Success) return NotFound();
            return Ok(response.InventoryQuarantine);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _mediator.Send(new DeleteInventoryQuarantineCommandRequest { Id = id });
            if (!response.Success) return NotFound();
            return NoContent();
        }
    }

}