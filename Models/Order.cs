using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketApplication.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

        [Required]
        public int MatchID { get; set; }
        [ForeignKey("MatchID")]
        public Match Match { get; set; }

        [Required]
        public int TicketQuantity { get; set; }

        [Required]
        public string TicketPlan { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
    }
}
