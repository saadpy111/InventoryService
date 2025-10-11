using System;
using System.Collections.Generic;

namespace Inventory.Application.Dtos.ProductDtos
{
    public class UpdateProductDto
    {
        public Guid Id { get; set; }
        public string? Sku { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }

        public List<UpdateProductAttributeValueDto>? AttributeValues { get; set; }
    }
}