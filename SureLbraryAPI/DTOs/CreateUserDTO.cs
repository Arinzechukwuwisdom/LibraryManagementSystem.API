using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SureLbraryAPI.DTOs
{
    public class CreateUserDTO
    {
#nullable disable
        public string Name { get; set; }
        public string Password { get; set; } 
        public string Email { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Role { get; set; }
    }
}
