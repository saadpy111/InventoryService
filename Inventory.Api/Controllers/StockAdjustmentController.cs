using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Dtos.StockAdjustmentDtos;
using Inventory.Application.Features.StockAdjustmentFeatures.Queries.GetPagedStockAdjustments;
using Inventory.Application.Features.StockAdjustmentFeatures.Queries.GetStockAdjustmentById;

namespace Inventory.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockAdjustmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockAdjustmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var response = await _mediator.Send(new GetPagedStockAdjustmentsQueryRequest
            {
                Search = search,
                Page = page,
                PageSize = pageSize
            });
            return Ok(response.Result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetStockAdjustmentByIdQueryRequest { Id = id });
            if (response.StockAdjustment == null) return NotFound();
            return Ok(response.StockAdjustment);
        }
    }
}