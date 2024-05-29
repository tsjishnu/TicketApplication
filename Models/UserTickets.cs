using System;

namespace TicketApplication.Models
{
    public class UserTickets
    {
        public Match Match { get; set; }
        public int TicketQuantity { get; set; }
        public string TicketPlan { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
