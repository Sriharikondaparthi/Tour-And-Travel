-- ============================================
-- Smart Travel Website - MySQL Database Setup
-- Run this in PHPMyAdmin SQL tab
-- ============================================

CREATE DATABASE IF NOT EXISTS SmartTravelDB CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE SmartTravelDB;

-- Users table
CREATE TABLE IF NOT EXISTS Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    Role VARCHAR(20) DEFAULT 'user',
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Packages table
CREATE TABLE IF NOT EXISTS Packages (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(150) NOT NULL,
    Description TEXT,
    PriceFrom DECIMAL(10,2),
    PriceTo DECIMAL(10,2),
    ImageUrl VARCHAR(255),
    MapUrl VARCHAR(500),
    HotelUrl VARCHAR(500),
    WikiUrl VARCHAR(500),
    IsActive BOOLEAN DEFAULT TRUE
);

-- Bookings table
CREATE TABLE IF NOT EXISTS Bookings (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserId INT NOT NULL,
    PackageId INT NOT NULL,
    TravelDate VARCHAR(50),
    NumberOfPeople INT DEFAULT 1,
    Status VARCHAR(30) DEFAULT 'pending',
    SpecialRequests TEXT,
    TotalPrice DECIMAL(10,2),
    BookedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (PackageId) REFERENCES Packages(Id)
);

-- Contact Messages table
CREATE TABLE IF NOT EXISTS ContactMessages (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL,
    Phone VARCHAR(20),
    Subject VARCHAR(200),
    Message TEXT,
    IsRead BOOLEAN DEFAULT FALSE,
    SentAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- ============================================
-- Seed Data
-- ============================================

-- Default Admin (password: Admin@123)
INSERT INTO Users (Name, Email, PasswordHash, Role) VALUES
('Admin', 'admin@smarttravel.com', '$2a$11$defaultHashReplaceWithActualBCryptHash', 'admin');

-- Travel Packages
INSERT INTO Packages (Name, Description, PriceFrom, PriceTo, ImageUrl, MapUrl, WikiUrl) VALUES
('Manali Package', 'Kullu Manali Shimla - popular destination for travelers and honeymoon couples seeking solitude and calmness.', 5999, 8999, 'images/img-1.jpg', 'https://maps.app.goo.gl/3XtweBi9Xp3dLpZx7', 'https://en.wikipedia.org/wiki/Manali'),
('Goa Package', 'Baga beach, nightlife with famous clubs like Brittos, Titos and Mambos. Water sports available.', 7999, 12999, 'images/img-2.jpg', 'https://maps.app.goo.gl/b13g6H6QJeTCuoWD7', 'https://en.wikipedia.org/wiki/Goa'),
('Delhi Package', 'Cultural diversity and rich heritage - temples, tombs, gardens, forts, museums and markets.', 2999, 8999, 'images/img-3.jpg', 'https://maps.app.goo.gl/Myah87kh1GynYhKf7', 'https://en.wikipedia.org/wiki/Delhi'),
('Jaipur Package', 'Forts and Palaces, Deserts, Traditional Villages, colorful cattle fairs and camel safaris.', 11999, 15999, 'images/img-4.jpg', 'https://maps.app.goo.gl/ghrTTobSLmtycAxz5', 'https://en.wikipedia.org/wiki/Jaipur'),
('Kerala Package', 'Greenery to the hills, hand-picked Kerala tour packages with customised options.', 4999, 9999, 'images/img-5.jpg', 'https://maps.app.goo.gl/8UdgXRVEc7dRurgw9', 'https://en.wikipedia.org/wiki/Kerala'),
('Darjeeling Package', 'Himalayan Railway, Tiger Hill, stunning gardens, hiking and boating. Famous Mall Road.', 20000, 25000, 'images/img-6.jpg', 'https://maps.app.goo.gl/2YvieiQvQtNQXxtLA', 'https://en.wikipedia.org/wiki/Darjeeling'),
('Tirupati Package', 'Pilgrimage to Sri Venkateswara Temple - sacred city in Andhra Pradesh, blessings from Lord Balaji.', 4999, 9999, 'images/img-7.jpg', 'https://maps.app.goo.gl/bG3n7yhpAWi84HNQ7', 'https://en.wikipedia.org/wiki/Tirupati'),
('Ayodhya Package', 'Birthplace of Lord Ram on the banks of Sarayu river, great spiritual significance for Hindus.', 9999, 14999, 'images/img-8.jpg', 'https://maps.app.goo.gl/73gkhR2NWJ69GhY26', 'https://en.wikipedia.org/wiki/Ayodhya'),
('Hyderabad Package', 'Charminar, Golconda Fort, Ramoji Film City - a blend of history, culture and modern attractions.', 9999, 19999, 'images/img-9.jpg', 'https://maps.app.goo.gl/ToLZJX8UmWTmKENf8', 'https://en.wikipedia.org/wiki/Hyderabad');
