using System.ComponentModel.DataAnnotations;

namespace TicketApplication.Models
{
    public class Match
    {
        [Key]
        public int MatchId { get; set; }

        [Required]
        public DateTime MatchDate { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        public string HomeTeam { get; set; }

        [Required]
        public string AwayTeam { get; set; }

        [Required]
        public decimal TicketPrice { get; set; }

        [Required]
        public int TotalTickets { get; set; }
    }
}
