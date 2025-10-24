using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SureLbraryAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; } 
        [DataType(DataType.Text), Column(TypeName = "Nvarchar(80)")]
        public required string Name { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public  string MembershipCode=> MembershipPrefix+MembershipNumber.ToString("D4");
        public int MembershipNumber { get; set; }
        public string MembershipPrefix { get; set; } = "MEM-";
        public string Address { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
        public required string Role { get; set; }= string.Empty;
        //public List<string> Roles { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        List<Transaction> transactions = new List<Transaction>();
    }
}
