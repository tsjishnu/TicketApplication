using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TicketApplication.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }   
        public string Role { get; set; }

    }
}
