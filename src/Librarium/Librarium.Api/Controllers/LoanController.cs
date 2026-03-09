using Librarium.Api.Contracts;
using Librarium.Data;
using Librarium.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Librarium.Api.Controllers
{
    [ApiController]
    [Route("api/loans")]
    public class LoansController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public LoansController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanRequest request)
        {
            var bookExists = await _context.Books.AnyAsync(x => x.Id == request.BookId);
            if (!bookExists)
            {
                return BadRequest(new { message = $"Book with id {request.BookId} does not exist." });
            }

            var memberExists = await _context.Members.AnyAsync(x => x.Id == request.MemberId);
            if (!memberExists)
            {
                return BadRequest(new { message = $"Member with id {request.MemberId} does not exist." });
            }

            var loan = new Loan
            {
                BookId = request.BookId,
                MemberId = request.MemberId,
                LoanDate = request.LoanDate,
                ReturnDate = request.ReturnDate
            };

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetLoansByMemberId),
                new { memberId = request.MemberId },
                new { loan.Id, loan.BookId, loan.MemberId, loan.LoanDate, loan.ReturnDate });
        }

        [HttpGet("{memberId:int}")]
        public async Task<IActionResult> GetLoansByMemberId(int memberId)
        {
            var memberExists = await _context.Members.AnyAsync(x => x.Id == memberId);
            if (!memberExists)
            {
                return NotFound(new { message = $"Member with id {memberId} was not found." });
            }

            var loans = await _context.Loans
                .AsNoTracking()
                .Include(x => x.Book)
                .Include(x => x.Member)
                .Where(x => x.MemberId == memberId)
                .OrderByDescending(x => x.LoanDate)
                .Select(x => new LoanResponse
                {
                    Id = x.Id,
                    BookId = x.BookId,
                    BookTitle = x.Book.Title,
                    MemberId = x.MemberId,
                    MemberName = x.Member.FirstName + " " + x.Member.LastName,
                    LoanDate = x.LoanDate,
                    ReturnDate = x.ReturnDate
                })
                .ToListAsync();

            return Ok(loans);
        }
    }
}