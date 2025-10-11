using Inventory.Application.Contracts.Persistence.Repositories;
using Inventory.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Features.ProductFeatures.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repositories<Product>();
            var entity = await repo.GetById(request.Id);
            if (entity == null)
                return new DeleteProductCommandResponse { Success = false };

            repo.Remove(entity);
            await _unitOfWork.CompleteAsync();
            return new DeleteProductCommandResponse { Success = true };
        }
    }
}