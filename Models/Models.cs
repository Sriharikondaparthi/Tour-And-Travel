using System.ComponentModel.DataAnnotations;

namespace SmartTravel.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = "";

        [Required, MaxLength(150)]
        public string Email { get; set; } = "";

        [Required]
        public string PasswordHash { get; set; } = "";

        public string Role { get; set; } = "user"; // "user" or "admin"

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }

    public class Package
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; }

        public string ImageUrl { get; set; } = "";
        public string MapUrl { get; set; } = "";
        public string HotelUrl { get; set; } = "";
        public string WikiUrl { get; set; } = "";

        public bool IsActive { get; set; } = true;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }

    public class Booking
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int PackageId { get; set; }
        public Package Package { get; set; } = null!;

        [Required]
        public string TravelDate { get; set; } = "";

        public int NumberOfPeople { get; set; } = 1;

        public string Status { get; set; } = "pending"; // pending, confirmed, cancelled, rejected

        public string? SpecialRequests { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime BookedAt { get; set; } = DateTime.UtcNow;
    }

    public class ContactMessage
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = "";

        [Required, MaxLength(150)]
        public string Email { get; set; } = "";

        [MaxLength(20)]
        public string Phone { get; set; } = "";

        [MaxLength(200)]
        public string Subject { get; set; } = "";

        public string Message { get; set; } = "";

        public bool IsRead { get; set; } = false;

        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
