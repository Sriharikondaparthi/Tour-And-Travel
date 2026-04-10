using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using SmartTravel.Data;
using SmartTravel.Models;

namespace SmartTravel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public BookingsController(AppDbContext db) => _db = db;

        private int GetUserId() =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequest req)
        {
            var package = await _db.Packages.FindAsync(req.PackageId);
            if (package == null) return NotFound(new { message = "Package not found." });

            var booking = new Booking
            {
                UserId = GetUserId(),
                PackageId = req.PackageId,
                TravelDate = req.TravelDate,
                NumberOfPeople = req.NumberOfPeople,
                SpecialRequests = req.SpecialRequests,
                TotalPrice = package.PriceFrom * req.NumberOfPeople,
                Status = "pending"
            };

            _db.Bookings.Add(booking);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Booking created successfully!", bookingId = booking.Id });
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyBookings()
        {
            var userId = GetUserId();
            var bookings = await _db.Bookings
                .Include(b => b.Package)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.BookedAt)
                .Select(b => new BookingResponse
                {
                    Id = b.Id,
                    PackageName = b.Package.Name,
                    TravelDate = b.TravelDate,
                    NumberOfPeople = b.NumberOfPeople,
                    Status = b.Status,
                    TotalPrice = b.TotalPrice,
                    BookedAt = b.BookedAt,
                    SpecialRequests = b.SpecialRequests
                })
                .ToListAsync();

            return Ok(bookings);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var booking = await _db.Bookings.FindAsync(id);
            if (booking == null || booking.UserId != GetUserId())
                return NotFound(new { message = "Booking not found." });

            booking.Status = "cancelled";
            await _db.SaveChangesAsync();
            return Ok(new { message = "Booking cancelled." });
        }

        // Admin endpoints
        [HttpGet("all")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _db.Bookings
                .Include(b => b.Package)
                .Include(b => b.User)
                .OrderByDescending(b => b.BookedAt)
                .Select(b => new
                {
                    b.Id,
                    UserName = b.User.Name,
                    UserEmail = b.User.Email,
                    PackageName = b.Package.Name,
                    b.TravelDate,
                    b.NumberOfPeople,
                    b.Status,
                    b.TotalPrice,
                    b.BookedAt
                })
                .ToListAsync();

            return Ok(bookings);
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] BookingStatusRequest req)
        {
            var booking = await _db.Bookings.FindAsync(id);
            if (booking == null) return NotFound();
            booking.Status = req.Status;
            await _db.SaveChangesAsync();
            return Ok(new { message = "Status updated." });
        }
    }
}
