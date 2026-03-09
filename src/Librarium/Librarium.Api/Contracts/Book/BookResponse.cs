

namespace Librarium.Api.Contracts
{
    public class BookResponse
    {
        public int BookId { get; set; }

        public string Title { get; set; } = null!;

        public string Isbn { get; set; } = null!;

        public int PublicationYear { get; set; }

        public List<AuthorResponse> Authors { get; set; } = new();
    }
}