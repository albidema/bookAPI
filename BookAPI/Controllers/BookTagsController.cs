using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookAPI.Models;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookTagsController : ControllerBase
    {
        private readonly BookAPIContext _context;

        public BookTagsController(BookAPIContext context)
        {
            _context = context;
        }

        // GET: api/BookTags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookTag>>> GetBookTag()
        {
            return await _context.BookTag.ToListAsync();
        }

        // GET: api/BookTags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookTag>> GetBookTag(int id)
        {
            var bookTag = await _context.BookTag.FindAsync(id);

            if (bookTag == null)
            {
                return NotFound();
            }

            return bookTag;
        }

        // PUT: api/BookTags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookTag(int id, BookTag bookTag)
        {
            if (id != bookTag.Id)
            {
                return BadRequest();
            }

            _context.Entry(bookTag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookTagExists(id))
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

        // POST: api/BookTags
        [HttpPost]
        public async Task<ActionResult<BookTag>> PostBookTag(BookTag bookTag)
        {
            _context.BookTag.Add(bookTag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookTag", new { id = bookTag.Id }, bookTag);
        }

        // DELETE: api/BookTags/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookTag>> DeleteBookTag(int id)
        {
            var bookTag = await _context.BookTag.FindAsync(id);
            if (bookTag == null)
            {
                return NotFound();
            }

            _context.BookTag.Remove(bookTag);
            await _context.SaveChangesAsync();

            return bookTag;
        }

        private bool BookTagExists(int id)
        {
            return _context.BookTag.Any(e => e.Id == id);
        }
    }
}
