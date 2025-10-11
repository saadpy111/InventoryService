using System;
using System.Collections.Generic;

namespace Inventory.Application.Dtos.ProductDtos
{
    public class GetProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<ProductAttributeValueDto>? AttributeValues { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}