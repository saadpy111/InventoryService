using Inventory.Application.Contracts.Persistence.Repositories;
using Inventory.Application.Dtos.InventoryQuarantineDtos;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Features.InventoryQuarantineFeatures.Commands.UpdateInventoryQuarantineStatus
{
    public class UpdateInventoryQuarantineStatusCommandHandler : IRequestHandler<UpdateInventoryQuarantineStatusCommandRequest, UpdateInventoryQuarantineStatusCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateInventoryQuarantineStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateInventoryQuarantineStatusCommandResponse> Handle(UpdateInventoryQuarantineStatusCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var quarantineRepo = _unitOfWork.Repositories<InventoryQuarantine>();
                var entity = await quarantineRepo.GetById(request.InventoryQuarantineId);
                if (entity == null)
                {
                    return new UpdateInventoryQuarantineStatusCommandResponse { Success = false };
                }

                // If status is Accepted or Released, update StockQuant
                if (request.NewStatus == QuarantineStatus.Approved)
                {
                    var stockRepo = _unitOfWork.Repositories<StockQuant>();
                    var stockQuant = (await stockRepo.GetAll(
                        s => s.ProductId == entity.ProductId && s.LocationId == entity.LocationId
                    )).FirstOrDefault();

                    if (stockQuant != null)
                    {
                        stockQuant.Quantity += entity.Quantity;
                        stockRepo.Update(stockQuant);
                    }
                }

                entity.Status = request.NewStatus;
                entity.UpdatedAt = DateTime.UtcNow;
                quarantineRepo.Update(entity);

                await _unitOfWork.CompleteAsync();

                var dto = new GetInventoryQuarantineDto
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
                };

                return new UpdateInventoryQuarantineStatusCommandResponse
                {
                    Success = true,
                    InventoryQuarantine = dto
                };
            }
            catch
            {
                return new UpdateInventoryQuarantineStatusCommandResponse { Success = false };
            }
        }
    }
}