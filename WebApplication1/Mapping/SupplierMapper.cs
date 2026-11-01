using Riok.Mapperly.Abstractions;
using WebApplication1.Models;
using WebApplication1.Models.DTO.Supplier;

namespace WebApplication1.Mapping
{
    [Mapper]
    public partial class SupplierMapper
    {
        public partial SupplierResponseDTO ToDto(Supplier source);

        [MapperIgnoreTarget(nameof(Supplier.Id))]
        public partial Supplier ToModel(SupplierCreateDTO dto);

        [MapperIgnoreTarget(nameof(Supplier.Id))]
        public partial void UpdateModel(
        SupplierUpdateDTO dto,
        [MappingTarget] Supplier source);
    }
}
