using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Entities
{
    public class InventoryQuarantine : BaseEntity
    {
        public int Quantity { get; set; }
        public DateTime QuarantineDate { get; set; }
        public string Status { get; set; } 
        public string SourceReference { get; set; } 

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid LocationId { get; set; }
        public Location Location { get; set; }
    }
}
