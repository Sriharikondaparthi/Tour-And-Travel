# 🌍 Smart Travel Website — ASP.NET Core + MySQL

A full-stack travel booking website converted from PHP to ASP.NET Core Web API with HTML/CSS/JS frontend.

---

## 🚀 Features
- ✅ User Login & Signup (JWT Authentication)
- ✅ Travel Package Booking System
- ✅ Admin Dashboard (Stats, Bookings, Users, Messages)
- ✅ AI Trip Planner (powered by Google Gemini AI)
- ✅ Contact Form (stored in MySQL)
- ✅ MySQL Database via PHPMyAdmin

---

## 🛠️ Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- MySQL Server (with PHPMyAdmin)
- Your Anthropic API Key (for AI Trip Planner)

---

## ⚙️ Setup Instructions

### Step 1: Database Setup
1. Open PHPMyAdmin
2. Click **SQL** tab
3. Paste and run the contents of `database_setup.sql`

### Step 2: Configure appsettings.json
Open `appsettings.json` and update:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=SmartTravelDB;User=root;Password=YOUR_MYSQL_PASSWORD;"
  },
  "Anthropic": {
    "ApiKey": "YOUR_ANTHROPIC_API_KEY"
  }
}
```

### Step 3: Copy Images
Copy your images folder from the original project:
```
Smart Travel/images/  →  SmartTravel/wwwroot/images/
```

### Step 4: Install Dependencies & Run
```bash
cd SmartTravel
dotnet restore
dotnet run
```

### Step 5: Open Browser
Go to: **http://localhost:5000**

---

## 🔐 Default Admin Login
- Email: `admin@smarttravel.com`
- Password: `Admin@123`

> ⚠️ Change the admin password after first login!

---

## 📁 Project Structure
```
SmartTravel/
├── Controllers/
│   ├── AuthController.cs        # Login, Register, JWT
│   ├── BookingsController.cs    # Booking CRUD
│   ├── OtherControllers.cs      # Contact, Packages, Admin
│   └── TripPlannerController.cs # AI Trip Planner
├── Models/
│   ├── Models.cs                # User, Package, Booking, Contact
│   └── DTOs.cs                  # Request/Response objects
├── Data/
│   └── AppDbContext.cs          # Entity Framework DB Context
├── wwwroot/
│   ├── index.html               # Full frontend
│   └── images/                  # (copy from original project)
├── appsettings.json             # Configuration
├── Program.cs                   # App entry point
├── database_setup.sql           # MySQL setup script
└── SmartTravel.csproj           # Project file
```

---

## 🔌 API Endpoints

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| POST | /api/auth/register | ❌ | Register user |
| POST | /api/auth/login | ❌ | Login & get JWT |
| GET | /api/packages | ❌ | Get all packages |
| POST | /api/bookings | ✅ User | Create booking |
| GET | /api/bookings/my | ✅ User | My bookings |
| DELETE | /api/bookings/{id} | ✅ User | Cancel booking |
| GET | /api/bookings/all | ✅ Admin | All bookings |
| POST | /api/contact | ❌ | Send message |
| GET | /api/contact | ✅ Admin | View messages |
| GET | /api/admin/stats | ✅ Admin | Dashboard stats |
| GET | /api/admin/users | ✅ Admin | All users |
| POST | /api/tripplanner/plan | ❌ | AI trip plan |

---

## 🔧 Troubleshooting

**MySQL connection error?**
- Check your password in `appsettings.json`
- Make sure MySQL is running on port 3306

**AI Planner not working?**
- Add your Anthropic API key in `appsettings.json`

**Images not showing?**
## 👨‍💻 Author
**Designed & Prepared by Srihari Kondaparthi**
