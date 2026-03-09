

namespace Librarium.Api.Contracts
{
    public class AuthorResponse
    {
        public int AuthorId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Biography { get; set; }
    }
}