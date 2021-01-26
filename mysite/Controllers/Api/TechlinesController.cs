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
    public class TechlinesController : ControllerBase
    {
        private readonly DataContext _context;

        public TechlinesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Techlines
        [HttpGet]
        public IEnumerable<Techline> GetTechlines()
        {
            return _context.Techlines;
        }

        // GET: api/Techlines/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTechline([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var techline = await _context.Techlines.FindAsync(id);

            if (techline == null)
            {
                return NotFound();
            }

            return Ok(techline);
        }

        // PUT: api/Techlines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTechline([FromRoute] int id, [FromBody] Techline techline)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != techline.Id)
            {
                return BadRequest();
            }

            _context.Entry(techline).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TechlineExists(id))
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

        // POST: api/Techlines
        [HttpPost]
        public async Task<IActionResult> PostTechline([FromBody] Techline techline)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Techlines.Add(techline);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTechline", new { id = techline.Id }, techline);
        }

        // DELETE: api/Techlines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTechline([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var techline = await _context.Techlines.FindAsync(id);
            if (techline == null)
            {
                return NotFound();
            }

            _context.Techlines.Remove(techline);
            await _context.SaveChangesAsync();

            return Ok(techline);
        }

        private bool TechlineExists(int id)
        {
            return _context.Techlines.Any(e => e.Id == id);
        }
    }
}