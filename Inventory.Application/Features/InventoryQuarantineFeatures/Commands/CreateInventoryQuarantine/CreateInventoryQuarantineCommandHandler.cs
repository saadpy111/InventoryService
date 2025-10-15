using Inventory.Application.Contracts.Persistence.Repositories;
using Inventory.Application.Dtos.InventoryQuarantineDtos;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Features.InventoryQuarantineFeatures.Commands.CreateInventoryQuarantine
{
    public class CreateInventoryQuarantineCommandHandler : IRequestHandler<CreateInventoryQuarantineCommandRequest, CreateInventoryQuarantineCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateInventoryQuarantineCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateInventoryQuarantineCommandResponse> Handle(CreateInventoryQuarantineCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = new InventoryQuarantine
                {
                    Quantity = request.InventoryQuarantine.Quantity,
                    QuarantineDate = request.InventoryQuarantine.QuarantineDate,
                    Status = request.InventoryQuarantine.Status,
                    SourceReference = request.InventoryQuarantine.SourceReference,
                    ProductId = request.InventoryQuarantine.ProductId,
                    LocationId = request.InventoryQuarantine.LocationId,
                    CreatedAt = DateTime.UtcNow
                };


                await _unitOfWork.Repositories<InventoryQuarantine>().Add(entity);


                var stockRepo = _unitOfWork.Repositories<StockQuant>();
            
                var stockQuant = (await stockRepo.GetAll
                    (
                       s =>s.LocationId == entity.LocationId &&
                                s.ProductId == entity.ProductId
                    )).FirstOrDefault();


                if(stockQuant==null || 
                   stockQuant.Quantity < request.InventoryQuarantine.Quantity)
                {
                    return new CreateInventoryQuarantineCommandResponse { Success = false };

                }
              
                stockQuant.Quantity -= request.InventoryQuarantine.Quantity;
                await _unitOfWork.CompleteAsync();

                return new CreateInventoryQuarantineCommandResponse
                {
                    Success = true,
                    InventoryQuarantine = new GetInventoryQuarantineDto
                    {
                        Id = entity.Id,
                        Quantity = entity.Quantity,
                        QuarantineDate = entity.QuarantineDate,
                        Status = entity.Status,
                        SourceReference = entity.SourceReference,
                        ProductId = entity.ProductId,
                        LocationId = entity.LocationId,
                        CreatedAt = entity.CreatedAt,
                        UpdatedAt = entity.UpdatedAt
                    }
                };
            }
            catch
            {
                return new CreateInventoryQuarantineCommandResponse { Success = false };
            }
        }
    }
}