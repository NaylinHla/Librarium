using Librarium.Api.Contracts;
using Librarium.Data;
using Librarium.Data.Entity;
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
                .Include(x => x.Authors)
                .OrderBy(x => x.Title)
                .Select(x => new BookResponse
                {
                    BookId = x.Id,
                    Title = x.Title,
                    Isbn = x.Isbn,
                    PublicationYear = x.PublicationYear,
                    Authors = x.Authors
                        .OrderBy(a => a.LastName)
                        .ThenBy(a => a.FirstName)
                        .Select(a => new AuthorResponse
                        {
                            AuthorId = a.Id,
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                            Biography = a.Biography
                        })
                        .ToList()
                })
                .ToListAsync();

            return Ok(books);
        }
        
        [HttpPost("{bookId:int}/authors")]
        public async Task<IActionResult> AddAuthorToBook(int bookId, [FromBody] AddAuthorToBookRequest request)
        {
            var book = await _context.Books
                .Include(x => x.Authors)
                .FirstAsync(x => x.Id == bookId);

            var author = new Author
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Biography = request.Biography
            };

            book.Authors.Add(author);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}