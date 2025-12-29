using SureLbraryAPI.Enums;

namespace SureLbraryAPI.DTOs
{
    public class GetTransactionDTO
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required TransactionStatus Status { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
        public string? BookTitle { get; set; }
        public DateTime BorrowDate { get; set; }
        public int? DaysOverdue{ get; set; }
    }
}
