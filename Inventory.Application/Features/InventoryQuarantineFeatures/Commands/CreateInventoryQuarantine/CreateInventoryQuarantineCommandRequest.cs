using Inventory.Application.Dtos.InventoryQuarantineDtos;
using MediatR;

namespace Inventory.Application.Features.InventoryQuarantineFeatures.Commands.CreateInventoryQuarantine
{
    public class CreateInventoryQuarantineCommandRequest : IRequest<CreateInventoryQuarantineCommandResponse>
    {
        public CreateInventoryQuarantineDto InventoryQuarantine { get; set; }
    }
}