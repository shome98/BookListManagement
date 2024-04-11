using BookListManagement.Server.Data;
using BookListManagement.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListManagement.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController:ControllerBase
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books=await _context.Books.ToListAsync();   
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(Book book)
        {
            if(ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return Ok(book);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook( int id,Book book)
        {
            if(id!=book.Id)
            {
                return BadRequest();
            }
            _context.Entry(book).State = EntityState.Modified; 
            try { 
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book=await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            return Ok(await _context.Books.FindAsync(id));
        }

        [HttpGet("Sort")]
        public async Task<IActionResult> SortByAuthor()
        {
            var bookSorted= await _context.Books.OrderByDescending(x => x.Author).ToListAsync();
            return Ok(bookSorted);
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(entry=>entry.Id==id);
        }
    }
}
