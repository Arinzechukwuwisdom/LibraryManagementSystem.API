using SureLbraryAPI.Enums;

namespace SureLbraryAPI.DTOs
{
    public class CreateTransactionDTO
    {  
        public required TransactionStatus Status { get; set; }
        public required DateTime ReturnDate { get; set; }
        public  DateTime? ExpectedReturnDate { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ActualReturnDate { get; set; }
    }
}
