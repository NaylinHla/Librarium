using Librarium.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Librarium.Api.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public BooksController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _context.Books
                .AsNoTracking()
                .OrderBy(x => x.Title)
                .ToListAsync();

            return Ok(books);
        }
    }
}