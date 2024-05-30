using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TicketApplication.Data;
using TicketApplication.Models;

namespace TicketApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly FootballDbContext _context;
        public UserController(FootballDbContext context)
        {
            _context = context;
        }

        [HttpGet("upcoming-matches")]
        public async Task<IActionResult> GetUpcomingMatches()
        {
            var today = DateTime.Today;
            var upcomingMatches = await _context.MatchTable
                .Where(m => m.MatchDate >= today)
                .OrderBy(m => m.MatchDate)
                .ToListAsync();
            return Ok(upcomingMatches);
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int userId = Int32.Parse(orderDto.UserId);

            var match = await _context.MatchTable.FindAsync(orderDto.MatchID);
            if (match == null)
            {
                return NotFound("Match not found");
            }
            match.TotalTickets = match.TotalTickets - orderDto.TicketQuantity;
            var order = new Order
            {
                UserID = userId,
                MatchID = orderDto.MatchID,
                TicketQuantity = orderDto.TicketQuantity,
                TicketPlan = orderDto.TicketPlan,
                TotalAmount = orderDto.TotalAmount,
                OrderDate = DateTime.Now
            };
            _context.OrderTable.Add(order);
            await _context.SaveChangesAsync();
            return Ok(order);
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<UserTickets>>> GetUserTickets(int userId)
        {
            var userTickets = await _context.OrderTable
                .Where(o => o.UserID == userId)
                .Include(o => o.Match) // Include the Match details
                .OrderBy(o => o.Match.MatchDate) 
                .Select(o => new UserTickets
                {
                    Match = o.Match,
                    TicketQuantity = o.TicketQuantity,
                    TicketPlan = o.TicketPlan,
                    TotalAmount = o.TotalAmount
                })
                .ToListAsync();

            return userTickets;
        }
    }
}
