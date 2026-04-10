namespace SmartTravel.Models
{
    // Auth DTOs
    public class RegisterRequest
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class LoginRequest
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class AuthResponse
    {
        public string Token { get; set; } = "";
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Role { get; set; } = "";
    }

    // Booking DTOs
    public class BookingRequest
    {
        public int PackageId { get; set; }
        public string TravelDate { get; set; } = "";
        public int NumberOfPeople { get; set; } = 1;
        public string? SpecialRequests { get; set; }
    }

    public class BookingStatusRequest
    {
        public string Status { get; set; } = "";
    }

    public class BookingResponse
    {
        public int Id { get; set; }
        public string PackageName { get; set; } = "";
        public string TravelDate { get; set; } = "";
        public int NumberOfPeople { get; set; }
        public string Status { get; set; } = "";
        public decimal TotalPrice { get; set; }
        public DateTime BookedAt { get; set; }
        public string? SpecialRequests { get; set; }
    }

    // Contact DTO
    public class ContactRequest
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Subject { get; set; } = "";
        public string Message { get; set; } = "";
    }

    // AI Trip Planner DTO
    public class TripPlanRequest
    {
        public string Destination { get; set; } = "";
        public string Duration { get; set; } = "";
        public string Budget { get; set; } = "";
        public string Interests { get; set; } = "";
        public int Travelers { get; set; } = 1;
    }

    // Admin Stats DTO
    public class AdminStats
    {
        public int TotalUsers { get; set; }
        public int TotalBookings { get; set; }
        public int PendingBookings { get; set; }
        public int UnreadMessages { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
