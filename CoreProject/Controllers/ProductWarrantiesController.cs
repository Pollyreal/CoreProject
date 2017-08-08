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
    [Route("api/ProductWarranties")]
    public class ProductWarrantiesController : Controller
    {
        private readonly WarrantyContext _context;

        public ProductWarrantiesController(WarrantyContext context)
        {
            _context = context;
        }

        // GET: api/ProductWarranties
        [HttpGet]
        public IEnumerable<ProductWarranty> GetProductWarranties()
        {
            return _context.ProductWarranties;
        }

        // GET: api/ProductWarranties/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductWarranty([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productWarranty = await _context.ProductWarranties.SingleOrDefaultAsync(m => m.ProductWarrantyId == id);

            if (productWarranty == null)
            {
                return NotFound();
            }

            return Ok(productWarranty);
        }

        // PUT: api/ProductWarranties/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductWarranty([FromRoute] int id, [FromBody] ProductWarranty productWarranty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productWarranty.ProductWarrantyId)
            {
                return BadRequest();
            }

            _context.Entry(productWarranty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductWarrantyExists(id))
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

        // POST: api/ProductWarranties
        [HttpPost]
        public async Task<IActionResult> PostProductWarranty([FromBody] ProductWarranty productWarranty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProductWarranties.Add(productWarranty);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductWarranty", new { id = productWarranty.ProductWarrantyId }, productWarranty);
        }

        // DELETE: api/ProductWarranties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductWarranty([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productWarranty = await _context.ProductWarranties.SingleOrDefaultAsync(m => m.ProductWarrantyId == id);
            if (productWarranty == null)
            {
                return NotFound();
            }

            _context.ProductWarranties.Remove(productWarranty);
            await _context.SaveChangesAsync();

            return Ok(productWarranty);
        }

        private bool ProductWarrantyExists(int id)
        {
            return _context.ProductWarranties.Any(e => e.ProductWarrantyId == id);
        }
    }
}