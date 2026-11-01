using Riok.Mapperly.Abstractions;
using WebApplication1.Models;
using WebApplication1.Models.DTO.Product;

namespace WebApplication1.Mapping
{
    [Mapper]
    public partial class ProductMapper
    {
        [MapProperty(nameof(Product.Category.Name),
               nameof(ProductResponseDTO.CategoryName))]

        [MapProperty(nameof(Product.Supplier.Name),
               nameof(ProductResponseDTO.SupplierName))]
        public partial ProductResponseDTO ToDto(Product source);

        [MapperIgnoreTarget(nameof(Product.Id))]
        [MapperIgnoreTarget(nameof(Product.Category))]
        [MapperIgnoreTarget(nameof(Product.Supplier))]
        public partial Product ToModel(ProductCreateDTO dto);

        [MapperIgnoreTarget(nameof(Product.Id))]
        [MapperIgnoreTarget(nameof(Product.Category))]
        [MapperIgnoreTarget(nameof(Product.Supplier))]
        public partial void UpdateModel(
        ProductUpdateDTO dto,
        [MappingTarget] Product source);
    }
}
