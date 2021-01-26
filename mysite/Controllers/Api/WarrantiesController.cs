using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mysite.Data;
using mysite.Models;

namespace mysite.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarrantiesController : ControllerBase
    {
        private readonly DataContext _context;

        public WarrantiesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Warranties
        [HttpGet]
        public IEnumerable<Warranty> GetWarranties()
        {
            return _context.Warranties;
        }

        // GET: api/Warranties/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarranty([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var warranty = await _context.Warranties.FindAsync(id);

            if (warranty == null)
            {
                return NotFound();
            }

            return Ok(warranty);
        }

        // PUT: api/Warranties/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarranty([FromRoute] int id, [FromBody] Warranty warranty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != warranty.Id)
            {
                return BadRequest();
            }

            _context.Entry(warranty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarrantyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Warranties
        [HttpPost]
        public async Task<IActionResult> PostWarranty([FromBody] Warranty warranty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Warranties.Add(warranty);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWarranty", new { id = warranty.Id }, warranty);
        }

        // DELETE: api/Warranties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarranty([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var warranty = await _context.Warranties.FindAsync(id);
            if (warranty == null)
            {
                return NotFound();
            }

            _context.Warranties.Remove(warranty);
            await _context.SaveChangesAsync();

            return Ok(warranty);
        }

        private bool WarrantyExists(int id)
        {
            return _context.Warranties.Any(e => e.Id == id);
        }
    }
}