using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Dtos.ProductDtos;
using Inventory.Application.Features.ProductFeatures.Commands.CreateProduct;
using Inventory.Application.Features.ProductFeatures.Commands.AddProductAttributeValues;
using Inventory.Application.Features.ProductFeatures.Queries.GetProductById;
using Inventory.Application.Features.ProductFeatures.Commands.DeleteProduct;
using Inventory.Application.Features.ProductFeatures.Commands.MakeProductInactive;
using Inventory.Application.Features.ProductFeatures.Commands.UpdateProduct;

namespace Inventory.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            var response = await _mediator.Send(new CreateProductCommandRequest { Product = dto });
            if (!response.Success) return BadRequest();
            return  Ok(response);
        }
            
        [HttpPost("{id}/attribute-values")]
        public async Task<IActionResult> AddAttributeValues(Guid id, [FromBody] List<ProductAttributeValueDto> values)
        {
            var response = await _mediator.Send(new AddProductAttributeValuesCommandRequest
            {
                ProductId = id,
                AttributeValues = values
            });
            if (!response.Success) return BadRequest();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetProductByIdQueryRequest { Id = id });
            if (response.Product == null) return NotFound();
            return Ok(response.Product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _mediator.Send(new DeleteProductCommandRequest { Id = id });
            if (!response.Success) return NotFound();
            return NoContent(); 
        }

        [HttpPut("{id}/inactive")]
        public async Task<IActionResult> MakeInactive(Guid id)
        {
            var response = await _mediator.Send(new MakeProductInactiveCommandRequest { Id = id });
            if (!response.Success) return NotFound();
            return NoContent(); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var response = await _mediator.Send(new UpdateProductCommandRequest { Product = dto });
            if (!response.Success) return NotFound();
            return Ok(response.Product);
        }
    }
}