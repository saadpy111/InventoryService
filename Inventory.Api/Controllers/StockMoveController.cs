using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Dtos.StockMoveDtos;
using Inventory.Application.Features.StockMoveFeatures.Commands.CreateStockMove;

using Inventory.Application.Features.StockMoveFeatures.Queries.GetStockMoveById;
using Inventory.Application.Features.StockMoveFeatures.Queries.GetAllStockMoves;
using Inventory.Domain.Enums;
using Inventory.Application.Features.StockMoveFeatures.Queries.GetPagedStockMoves;

namespace Inventory.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockMoveController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockMoveController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid? productId)
        {
            var response = await _mediator.Send(new GetAllStockMovesQueryRequest { ProductId = productId });
            return Ok(response.StockMoves);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetStockMoveByIdQueryRequest { Id = id });
            if (response.StockMove == null) return NotFound();
            return Ok(response.StockMove);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockMoveDto dto)
        {
            var response = await _mediator.Send(new CreateStockMoveCommandRequest { StockMove = dto });
            if (!response.Success) return BadRequest();
            return Ok(response.StockMove);
        }

        [HttpGet("StockMoveTypes")]
        public IActionResult GetStockMoveTypes()
        {
            var values = Enum.GetValues(typeof(StockMoveType))
                .Cast<StockMoveType>()
                .Select(e => new
                {
                    Id = (int)e,
                    Name = e.ToString()
                });

            return Ok(values);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var response = await _mediator.Send(new GetPagedStockMovesQueryRequest
            {
                Search = search,
                Page = page,
                PageSize = pageSize
            });
            return Ok(response.Result);
        }
    }
}