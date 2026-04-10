using Microsoft.EntityFrameworkCore;
using SmartTravel.Models;

namespace SmartTravel.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed default admin
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Name = "Admin",
                Email = "admin@smarttravel.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                Role = "admin",
                CreatedAt = DateTime.UtcNow
            });

            // Seed packages
            modelBuilder.Entity<Package>().HasData(
                new Package { Id = 1, Name = "Manali Package", Description = "Kullu Manali Shimla - popular destination for travelers and honeymoon couples seeking solitude and calmness.", PriceFrom = 5999, PriceTo = 8999, ImageUrl = "images/img-1.jpg", MapUrl = "https://maps.app.goo.gl/3XtweBi9Xp3dLpZx7", WikiUrl = "https://en.wikipedia.org/wiki/Manali" },
                new Package { Id = 2, Name = "Goa Package", Description = "Baga beach, nightlife with famous clubs like Brittos, Titos and Mambos. Water sports available.", PriceFrom = 7999, PriceTo = 12999, ImageUrl = "images/img-2.jpg", MapUrl = "https://maps.app.goo.gl/b13g6H6QJeTCuoWD7", WikiUrl = "https://en.wikipedia.org/wiki/Goa" },
                new Package { Id = 3, Name = "Delhi Package", Description = "Cultural diversity and rich heritage - temples, tombs, gardens, forts, museums and markets.", PriceFrom = 2999, PriceTo = 8999, ImageUrl = "images/img-3.jpg", MapUrl = "https://maps.app.goo.gl/Myah87kh1GynYhKf7", WikiUrl = "https://en.wikipedia.org/wiki/Delhi" },
                new Package { Id = 4, Name = "Jaipur Package", Description = "Forts and Palaces, Deserts, Traditional Villages, colorful cattle fairs and camel safaris.", PriceFrom = 11999, PriceTo = 15999, ImageUrl = "images/img-4.jpg", MapUrl = "https://maps.app.goo.gl/ghrTTobSLmtycAxz5", WikiUrl = "https://en.wikipedia.org/wiki/Jaipur" },
                new Package { Id = 5, Name = "Kerala Package", Description = "Greenery to the hills, hand-picked Kerala tour packages with customised options.", PriceFrom = 4999, PriceTo = 9999, ImageUrl = "images/img-5.jpg", MapUrl = "https://maps.app.goo.gl/8UdgXRVEc7dRurgw9", WikiUrl = "https://en.wikipedia.org/wiki/Kerala" },
                new Package { Id = 6, Name = "Darjeeling Package", Description = "Himalayan Railway, Tiger Hill, stunning gardens, hiking and boating. Famous Mall Road.", PriceFrom = 20000, PriceTo = 25000, ImageUrl = "images/img-6.jpg", MapUrl = "https://maps.app.goo.gl/2YvieiQvQtNQXxtLA", WikiUrl = "https://en.wikipedia.org/wiki/Darjeeling" },
                new Package { Id = 7, Name = "Tirupati Package", Description = "Pilgrimage to Sri Venkateswara Temple - sacred city in Andhra Pradesh, blessings from Lord Balaji.", PriceFrom = 4999, PriceTo = 9999, ImageUrl = "images/img-7.jpg", MapUrl = "https://maps.app.goo.gl/bG3n7yhpAWi84HNQ7", WikiUrl = "https://en.wikipedia.org/wiki/Tirupati" },
                new Package { Id = 8, Name = "Ayodhya Package", Description = "Birthplace of Lord Ram on the banks of Sarayu river, great spiritual significance for Hindus.", PriceFrom = 9999, PriceTo = 14999, ImageUrl = "images/img-8.jpg", MapUrl = "https://maps.app.goo.gl/73gkhR2NWJ69GhY26", WikiUrl = "https://en.wikipedia.org/wiki/Ayodhya" },
                new Package { Id = 9, Name = "Hyderabad Package", Description = "Charminar, Golconda Fort, Ramoji Film City - a blend of history, culture and modern attractions.", PriceFrom = 9999, PriceTo = 19999, ImageUrl = "images/img-9.jpg", MapUrl = "https://maps.app.goo.gl/ToLZJX8UmWTmKENf8", WikiUrl = "https://en.wikipedia.org/wiki/Hyderabad" }
            );
        }
    }
}
