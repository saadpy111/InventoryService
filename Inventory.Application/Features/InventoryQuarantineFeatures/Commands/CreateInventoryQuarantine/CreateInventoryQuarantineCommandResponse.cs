using Inventory.Application.Dtos.InventoryQuarantineDtos;

namespace Inventory.Application.Features.InventoryQuarantineFeatures.Commands.CreateInventoryQuarantine
{
    public class CreateInventoryQuarantineCommandResponse
    {
        public bool Success { get; set; }
        public GetInventoryQuarantineDto? InventoryQuarantine { get; set; }
    }
}