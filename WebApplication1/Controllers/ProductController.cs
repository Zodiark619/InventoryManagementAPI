using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Mapping;
using WebApplication1.Models.DTO.Product;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductMapper _productMapper;

        public ProductController(ApplicationDbContext context, ProductMapper productMapper)
        {
            _context = context;
            _productMapper = productMapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Product
                .Include(x=>x.Supplier).Include(x=>x.Category)
                .ToListAsync();

            var result = data
                .Select(x => _productMapper.ToDto(x))
                .ToList();

            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var data = await _context.Product
                .Include(x => x.Supplier).Include(x => x.Category)

        .FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                return NotFound();

            var result = _productMapper.ToDto(data);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDTO dto)
        {

            var data = _productMapper.ToModel(dto);

            _context.Product.Add(data);
            await _context.SaveChangesAsync();
            var model = await _context.Product
    .Include(p => p.Category)
    .Include(p => p.Supplier)
    .FirstAsync(p => p.Id == data.Id);
            var result = _productMapper.ToDto(model);

            return CreatedAtAction(nameof(Get), new { id = data.Id }, result);



        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ProductUpdateDTO dto)
        {
            var data = await _context.Product
                .Include(x => x.Supplier).Include(x => x.Category)

       .FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                return NotFound();
            _productMapper.UpdateModel(dto, data);

            await _context.SaveChangesAsync();
            
            var result = _productMapper.ToDto(data);
            return Ok(result);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.Product
                .Include(x => x.Supplier).Include(x => x.Category)

       .FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                return NotFound();
 
            var result = _productMapper.ToDto(data);
            _context.Product.Remove(data);
            await _context.SaveChangesAsync();
            return Ok(result);
        }
    }
}
