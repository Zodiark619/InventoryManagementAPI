using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Mapping;
using WebApplication1.Models.DTO.Category;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly CategoryMapper _categoryMapper;

        public CategoryController(ApplicationDbContext context,CategoryMapper categoryMapper)
        {
            _context = context;
            _categoryMapper = categoryMapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]

        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Category.ToListAsync();

            var result = data
                .Select(x => _categoryMapper.ToDto(x))
                .ToList();

            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin,User")]

        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var data = await _context.Category
        .FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                return NotFound();

            var result = _categoryMapper.ToDto(data);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create(CategoryCreateDTO dto)
        {

            var data = _categoryMapper.ToModel(dto);

            _context.Category.Add(data);
            await _context.SaveChangesAsync();
            var result = _categoryMapper.ToDto(data);

            return CreatedAtAction(nameof(Get), new { id = data.Id }, result);



        }
        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CategoryUpdateDTO dto)
        {
            var data = await _context.Category
       .FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                return NotFound();
            _categoryMapper.UpdateModel(dto, data);

            await _context.SaveChangesAsync();
            var result = _categoryMapper.ToDto(data);

            return Ok(result);
        }
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.Category
       .FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                return NotFound();

            var result = _categoryMapper.ToDto(data);
            _context.Category.Remove(data);
            await _context.SaveChangesAsync();
            return Ok(result);
        }

    }
}
