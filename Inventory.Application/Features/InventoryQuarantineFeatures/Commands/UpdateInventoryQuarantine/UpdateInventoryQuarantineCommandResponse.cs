using Inventory.Application.Dtos.InventoryQuarantineDtos;

namespace Inventory.Application.Features.InventoryQuarantineFeatures.Commands.UpdateInventoryQuarantine
{
    public class UpdateInventoryQuarantineCommandResponse
    {
        public bool Success { get; set; }
        public GetInventoryQuarantineDto? InventoryQuarantine { get; set; }
    }
}