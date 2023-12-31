﻿using Medium.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Medium.DA.Context
{
    public static class ModelBuilderExtensions
    {

        public static void SeedRolesAndUsers(this ModelBuilder modelBuilder)
        {

            // Seed roles
            modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<int> { Id = 2, Name = "User", NormalizedName = "USER" }
            );

            // Seed users
            var hasher = new PasswordHasher<ApplicationUser>();

            var admin = new ApplicationUser
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@meduim.com",
                NormalizedEmail = "ADMIN@MEDIUM.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Aa@123"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            modelBuilder.Entity<ApplicationUser>().HasData(admin);

            modelBuilder.Entity<Publisher>().HasData(new Publisher { Id = 1, Name = admin.UserName });

            var userRoles = new List<IdentityUserRole<int>>
            {
                new IdentityUserRole<int>() {  RoleId = 1, UserId = 1 },
                new IdentityUserRole<int>() {  RoleId = 2, UserId = 1 }
            };
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(userRoles);

        }
    }
}