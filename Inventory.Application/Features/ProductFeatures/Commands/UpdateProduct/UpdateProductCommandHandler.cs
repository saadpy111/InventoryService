using Inventory.Application.Contracts.Persistence.Repositories;
using Inventory.Application.Dtos.ProductDtos;
using Inventory.Domain.Entities;
using MediatR;
using System.Linq;

namespace Inventory.Application.Features.ProductFeatures.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var repo = _unitOfWork.Repositories<Product>();
                var product = await repo.GetById(request.Product.Id, p => p.AttributeValues);
                if (product == null)
                    return new UpdateProductCommandResponse { Success = false };

                product.Name = request.Product.Name;
                product.Description = request.Product.Description;
                product.UpdatedAt = DateTime.UtcNow;
                if( product.SalePrice != request.Product.SalePrice)
                {
                    await _unitOfWork.Repositories<ProductCostHistory>().Add(new ProductCostHistory()
                    {
                              NewCost = request.Product.CostPrice,
                              OldCost = product.CostPrice,
                              CreatedAt = DateTime.UtcNow,
                              ProductId = product.Id,      
                    });
                }
                var attributValues = new List<ProductAttributeValue>();
                attributValues = request.Product.AttributeValues.Select(p => new ProductAttributeValue()
                {
                    Id = p.Id,
                    AttributeId = p.AttributeId,
                    ProductId = p.ProductId,
                    Value = p.Value

                }).ToList();
                // Update attribute values
                product.AttributeValues = attributValues;
                repo.Update(product);

                await _unitOfWork.CompleteAsync();

                // Prepare response DTO
                var updatedAttrValues = request.Product.AttributeValues ?? new List<UpdateProductAttributeValueDto>();
                var dto = new GetProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    CreatedAt = product.CreatedAt,
                    UpdatedAt = product.UpdatedAt,

                };

                return new UpdateProductCommandResponse
                {
                    Success = true,
                    Product = dto
                };
            }
            catch
            {
                return new UpdateProductCommandResponse() { Success = false };
            }
        }
    }
}