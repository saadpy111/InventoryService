using Inventory.Application.Contracts.Persistence.Repositories;
using Inventory.Application.Dtos.InventoryQuarantineDtos;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Features.InventoryQuarantineFeatures.Commands.UpdateInventoryQuarantine
{
    public class UpdateInventoryQuarantineCommandHandler : IRequestHandler<UpdateInventoryQuarantineCommandRequest, UpdateInventoryQuarantineCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateInventoryQuarantineCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateInventoryQuarantineCommandResponse> Handle(UpdateInventoryQuarantineCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var repo = _unitOfWork.Repositories<InventoryQuarantine>();
                var entity = await repo.GetById(request.InventoryQuarantine.Id);
                if (entity == null)
                    return new UpdateInventoryQuarantineCommandResponse { Success = false };

                entity.Quantity = request.InventoryQuarantine.Quantity;
                entity.QuarantineDate = request.InventoryQuarantine.QuarantineDate;
                entity.Status = request.InventoryQuarantine.Status;
                entity.SourceReference = request.InventoryQuarantine.SourceReference;
                entity.ProductId = request.InventoryQuarantine.ProductId;
                entity.LocationId = request.InventoryQuarantine.LocationId;
                entity.UpdatedAt = DateTime.UtcNow;

                repo.Update(entity);
                await _unitOfWork.CompleteAsync();

                return new UpdateInventoryQuarantineCommandResponse
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
                return new UpdateInventoryQuarantineCommandResponse { Success = false };

            }

        }
    }
}