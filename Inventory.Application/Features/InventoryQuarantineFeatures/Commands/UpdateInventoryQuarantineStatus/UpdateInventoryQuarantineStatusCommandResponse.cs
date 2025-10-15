using Inventory.Application.Dtos.InventoryQuarantineDtos;

namespace Inventory.Application.Features.InventoryQuarantineFeatures.Commands.UpdateInventoryQuarantineStatus
{
    public class UpdateInventoryQuarantineStatusCommandResponse
    {
        public bool Success { get; set; }
        public GetInventoryQuarantineDto? InventoryQuarantine { get; set; }
    }
}