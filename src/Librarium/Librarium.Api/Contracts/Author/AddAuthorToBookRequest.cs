

namespace Librarium.Api.Contracts
{
    public class AddAuthorToBookRequest
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Biography { get; set; }
    }
}