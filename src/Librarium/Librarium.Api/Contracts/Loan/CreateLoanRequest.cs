namespace Librarium.Api.Contracts
{
    public class CreateLoanRequest
    {
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}