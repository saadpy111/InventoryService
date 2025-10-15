using Inventory.Application.Dtos.InventoryQuarantineDtos;
using MediatR;

namespace Inventory.Application.Features.InventoryQuarantineFeatures.Commands.UpdateInventoryQuarantine
{
    public class UpdateInventoryQuarantineCommandRequest : IRequest<UpdateInventoryQuarantineCommandResponse>
    {
        public UpdateInventoryQuarantineDto InventoryQuarantine { get; set; }
    }
}