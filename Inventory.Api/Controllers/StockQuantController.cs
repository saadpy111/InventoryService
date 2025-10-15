using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Dtos.StockQuantDtos;

using Inventory.Application.Features.StockQuantFeatures.Queries.GetStockQuantById;
using Inventory.Application.Features.StockQuantFeatures.Queries.GetAllStockQuants;

namespace Inventory.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockQuantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockQuantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid? productId, [FromQuery] Guid? locationId)
        {
            var response = await _mediator.Send(new GetAllStockQuantsQueryRequest { ProductId = productId, LocationId = locationId });
            return Ok(response.StockQuants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetStockQuantByIdQueryRequest { Id = id });
            if (response.StockQuant == null) return NotFound();
            return Ok(response.StockQuant);
        }


    }
}