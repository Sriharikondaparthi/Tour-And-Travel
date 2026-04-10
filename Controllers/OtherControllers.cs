using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartTravel.Data;
using SmartTravel.Models;

namespace SmartTravel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ContactController(AppDbContext db) => _db = db;

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] ContactRequest req)
        {
            var msg = new ContactMessage
            {
                Name = req.Name,
                Email = req.Email,
                Phone = req.Phone,
                Subject = req.Subject,
                Message = req.Message
            };
            _db.ContactMessages.Add(msg);
            await _db.SaveChangesAsync();
            return Ok(new { message = "Message sent successfully!" });
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetMessages()
        {
            var messages = await _db.ContactMessages
                .OrderByDescending(m => m.SentAt)
                .ToListAsync();
            return Ok(messages);
        }

        [HttpPut("{id}/read")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> MarkRead(int id)
        {
            var msg = await _db.ContactMessages.FindAsync(id);
            if (msg == null) return NotFound();
            msg.IsRead = true;
            await _db.SaveChangesAsync();
            return Ok(new { message = "Marked as read." });
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class PackagesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public PackagesController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetPackages() =>
            Ok(await _db.Packages.Where(p => p.IsActive).ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPackage(int id)
        {
            var pkg = await _db.Packages.FindAsync(id);
            return pkg == null ? NotFound() : Ok(pkg);
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _db;
        public AdminController(AppDbContext db) => _db = db;

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var stats = new AdminStats
            {
                TotalUsers = await _db.Users.CountAsync(u => u.Role == "user"),
                TotalBookings = await _db.Bookings.CountAsync(),
                PendingBookings = await _db.Bookings.CountAsync(b => b.Status == "pending"),
                UnreadMessages = await _db.ContactMessages.CountAsync(m => !m.IsRead),
                TotalRevenue = await _db.Bookings
                    .Where(b => b.Status == "confirmed")
                    .SumAsync(b => b.TotalPrice)
            };
            return Ok(stats);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _db.Users
                .Select(u => new { u.Id, u.Name, u.Email, u.Role, u.CreatedAt })
                .ToListAsync();
            return Ok(users);
        }
    }
}
