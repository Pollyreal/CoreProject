using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreProject.EntityFrameworkCore;
using CoreProject.EntityFrameworkCore.Models;

namespace CoreProject.Controllers
{
    [Produces("application/json")]
    [Route("api/WarrantyOwners")]
    public class WarrantyOwnersController : Controller
    {
        private readonly WarrantyContext _context;

        public WarrantyOwnersController(WarrantyContext context)
        {
            _context = context;
        }

        // GET: api/WarrantyOwners
        [HttpGet]
        public IEnumerable<WarrantyOwner> GetWarrantyOwners()
        {
            return _context.WarrantyOwners;
        }

        // GET: api/WarrantyOwners/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarrantyOwner([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var warrantyOwner = await _context.WarrantyOwners.SingleOrDefaultAsync(m => m.WarrantyOwnerId == id);

            if (warrantyOwner == null)
            {
                return NotFound();
            }

            return Ok(warrantyOwner);
        }

        // PUT: api/WarrantyOwners/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarrantyOwner([FromRoute] int id, [FromBody] WarrantyOwner warrantyOwner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != warrantyOwner.WarrantyOwnerId)
            {
                return BadRequest();
            }

            _context.Entry(warrantyOwner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarrantyOwnerExists(id))
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

        // POST: api/WarrantyOwners
        [HttpPost]
        public async Task<IActionResult> PostWarrantyOwner([FromBody] WarrantyOwner warrantyOwner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.WarrantyOwners.Add(warrantyOwner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWarrantyOwner", new { id = warrantyOwner.WarrantyOwnerId }, warrantyOwner);
        }

        // DELETE: api/WarrantyOwners/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarrantyOwner([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var warrantyOwner = await _context.WarrantyOwners.SingleOrDefaultAsync(m => m.WarrantyOwnerId == id);
            if (warrantyOwner == null)
            {
                return NotFound();
            }

            _context.WarrantyOwners.Remove(warrantyOwner);
            await _context.SaveChangesAsync();

            return Ok(warrantyOwner);
        }

        private bool WarrantyOwnerExists(int id)
        {
            return _context.WarrantyOwners.Any(e => e.WarrantyOwnerId == id);
        }
    }
}