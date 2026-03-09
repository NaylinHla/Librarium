

namespace Librarium.Data.Entity
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Isbn { get; set; } = null!;

        public int PublicationYear { get; set; }

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}