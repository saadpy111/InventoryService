using Inventory.Application.Contracts.Persistence.Repositories;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Features.InventoryQuarantineFeatures.Commands.DeleteInventoryQuarantine
{
    public class DeleteInventoryQuarantineCommandHandler : IRequestHandler<DeleteInventoryQuarantineCommandRequest, DeleteInventoryQuarantineCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteInventoryQuarantineCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteInventoryQuarantineCommandResponse> Handle(DeleteInventoryQuarantineCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var repo = _unitOfWork.Repositories<InventoryQuarantine>();
                var entity = await repo.GetById(request.Id);
                if (entity == null)
                    return new DeleteInventoryQuarantineCommandResponse { Success = false };

                repo.Remove(entity);
                await _unitOfWork.CompleteAsync();
                return new DeleteInventoryQuarantineCommandResponse { Success = true };

            }
            catch
            {
                return new DeleteInventoryQuarantineCommandResponse { Success = false };

            }
        }
    }
}