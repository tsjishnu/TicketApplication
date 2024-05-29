namespace TicketApplication.Models
{
    public class OrderCreateDto
    {
        public string UserId { get; set; }
        public int MatchID { get; set; }
        public int TicketQuantity { get; set; }
        public string TicketPlan { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
