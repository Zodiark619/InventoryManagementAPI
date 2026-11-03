using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Mapping;
using WebApplication1.Models.DTO.Supplier;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 

    public class SupplierController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly SupplierMapper _supplierMapper;

        public SupplierController(ApplicationDbContext context, SupplierMapper supplierMapper)
        {
            _context = context;
            _supplierMapper = supplierMapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]

        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Supplier.ToListAsync();

            var result = data
                .Select(x => _supplierMapper.ToDto(x))
                .ToList();

            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin,User")]

        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var data = await _context.Supplier
        .FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                return NotFound();

            var result = _supplierMapper.ToDto(data);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create(SupplierCreateDTO dto)
        {

            var data = _supplierMapper.ToModel(dto);

            _context.Supplier.Add(data);
            await _context.SaveChangesAsync();
            var result = _supplierMapper.ToDto(data);

            return CreatedAtAction(nameof(Get), new { id = data.Id }, result);



        }
        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] SupplierUpdateDTO dto)
        {
            var data = await _context.Supplier
       .FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                return NotFound();
            _supplierMapper.UpdateModel(dto, data);

            await _context.SaveChangesAsync();
            var result = _supplierMapper.ToDto(data);

            return Ok(result);
        }
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.Supplier
       .FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                return NotFound();

            var result = _supplierMapper.ToDto(data);
            _context.Supplier.Remove(data);
            await _context.SaveChangesAsync();
            return Ok(result);
        }
    }
}
