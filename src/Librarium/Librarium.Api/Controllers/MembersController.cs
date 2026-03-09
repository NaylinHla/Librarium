using Librarium.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Librarium.Api.Controllers
{
    [ApiController]
    [Route("api/members")]
    public class MembersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public MembersController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            var members = await _context.Members
                .AsNoTracking()
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ToListAsync();

            return Ok(members);
        }
    }
}