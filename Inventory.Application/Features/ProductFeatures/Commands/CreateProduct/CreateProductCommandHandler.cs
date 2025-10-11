using Inventory.Application.Contracts.Persistence.Repositories;
using Inventory.Application.Dtos.ProductDtos;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Features.ProductFeatures.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = new Product
                {
                    Name = request.Product.Name,
                    SalePrice = request.Product.SalePrice,
                    IsActive = true,
                    UnitOfMeasure = request.Product.UnitOfMeasure,
                    CostPrice = request.Product.CostPrice,
                    Sku = request.Product.Sku,
                    CategoryId = request.Product.CategoryId,
                    Description = request.Product.Description??"",
                    CreatedAt = DateTime.UtcNow
                };

                var AttributeValues = new List<ProductAttributeValue>();
                // Add attribute values if provided
                if (request.Product.AttributeValues != null && request.Product.AttributeValues.Count > 0)
                {
                    foreach (var attr in request.Product.AttributeValues)
                    {
                        var attrValue = new ProductAttributeValue
                        {
                            ProductId = entity.Id,
                            AttributeId = attr.AttributeId,
                            Value = attr.Value,
                            CreatedAt = DateTime.UtcNow

                        };
                        AttributeValues.Add(attrValue);
                    }

                    entity.AttributeValues = AttributeValues;
                    await _unitOfWork.Repositories<Product>().Add(entity);
                    await _unitOfWork.CompleteAsync();
                }

                // Prepare DTO
                var dto = new GetProductDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt,
                    AttributeValues = request.Product.AttributeValues
                };

                return new CreateProductCommandResponse
                {
                    Success = true,
                    Product = dto
                };
            }
            catch
            {
                return new CreateProductCommandResponse { Success = false };
            }
        }
    }
}