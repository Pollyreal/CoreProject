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
    [Route("api/WarrantyCards")]
    public class WarrantyCardsController : Controller
    {
        private readonly WarrantyContext _context;

        public WarrantyCardsController(WarrantyContext context)
        {
            _context = context;
        }

        // GET: api/WarrantyCards
        [HttpGet]
        public IEnumerable<WarrantyCard> GetWarrantyCards()
        {
            return _context.WarrantyCards;
        }

        // GET: api/WarrantyCards/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarrantyCard([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var warrantyCard = await _context.WarrantyCards.SingleOrDefaultAsync(m => m.WarrantyCardId == id);

            if (warrantyCard == null)
            {
                return NotFound();
            }

            return Ok(warrantyCard);
        }

        // PUT: api/WarrantyCards/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarrantyCard([FromRoute] int id, [FromBody] WarrantyCard warrantyCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != warrantyCard.WarrantyCardId)
            {
                return BadRequest();
            }

            _context.Entry(warrantyCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarrantyCardExists(id))
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

        // POST: api/WarrantyCards
        [HttpPost]
        public async Task<IActionResult> PostWarrantyCard([FromBody] WarrantyCard warrantyCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.WarrantyCards.Add(warrantyCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWarrantyCard", new { id = warrantyCard.WarrantyCardId }, warrantyCard);
        }

        // DELETE: api/WarrantyCards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarrantyCard([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var warrantyCard = await _context.WarrantyCards.SingleOrDefaultAsync(m => m.WarrantyCardId == id);
            if (warrantyCard == null)
            {
                return NotFound();
            }

            _context.WarrantyCards.Remove(warrantyCard);
            await _context.SaveChangesAsync();

            return Ok(warrantyCard);
        }

        private bool WarrantyCardExists(int id)
        {
            return _context.WarrantyCards.Any(e => e.WarrantyCardId == id);
        }
    }
}