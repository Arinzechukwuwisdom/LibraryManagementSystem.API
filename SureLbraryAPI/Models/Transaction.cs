using SureLbraryAPI.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SureLbraryAPI.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public TransactionStatus Status { get; set; }
        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(Book))]
        public int? BookId { get; set; }
        public Book Book { get; set; }
    }
}
