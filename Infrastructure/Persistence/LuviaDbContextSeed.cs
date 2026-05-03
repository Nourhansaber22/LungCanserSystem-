using Domain.Entities;
using Domain.Enums;
using Luvia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

public static class LuviaDbContextSeed
{
    public static void Seed(this LuviaDbContext context)
    {
        // ===============================
        // Seed Admin User
        // ===============================
        if (!context.Users.Any())
        {
            var admin = new User
            {
                Name = "Admin",
                Email = "admin@luvia.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = UserRole.Admin,
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            context.Users.Add(admin);
        }


        context.SaveChanges();
    }
}