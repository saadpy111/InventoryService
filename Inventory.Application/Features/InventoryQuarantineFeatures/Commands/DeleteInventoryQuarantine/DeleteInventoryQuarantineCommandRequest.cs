using MediatR;

namespace Inventory.Application.Features.InventoryQuarantineFeatures.Commands.DeleteInventoryQuarantine
{
    public class DeleteInventoryQuarantineCommandRequest : IRequest<DeleteInventoryQuarantineCommandResponse>
    {
        public Guid Id { get; set; }
    }
}