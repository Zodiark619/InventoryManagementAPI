using Riok.Mapperly.Abstractions;
using WebApplication1.Models;
using WebApplication1.Models.DTO.Category;

namespace WebApplication1.Mapping
{
    [Mapper]
    public partial class CategoryMapper
    {
        public partial CategoryResponseDTO ToDto(Category source);

        [MapperIgnoreTarget(nameof(Category.Id))]
        public partial Category ToModel(CategoryCreateDTO dto);

        [MapperIgnoreTarget(nameof(Category.Id))]
        public partial void UpdateModel(
        CategoryUpdateDTO dto,
        [MappingTarget] Category source);
    }
}
