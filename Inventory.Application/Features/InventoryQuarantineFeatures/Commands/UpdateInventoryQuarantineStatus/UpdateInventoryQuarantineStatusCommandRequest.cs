using Inventory.Domain.Enums;
using MediatR;
using System;

namespace Inventory.Application.Features.InventoryQuarantineFeatures.Commands.UpdateInventoryQuarantineStatus
{
    public class UpdateInventoryQuarantineStatusCommandRequest : IRequest<UpdateInventoryQuarantineStatusCommandResponse>
    {
        public Guid InventoryQuarantineId { get; set; }
        public QuarantineStatus NewStatus { get; set; }
    }
}