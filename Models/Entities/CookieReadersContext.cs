﻿using Microsoft.EntityFrameworkCore;

namespace Midterm_Assignment1_Login.Models.Entities
{
    public class CookieReadersContext : DbContext
    {
        public CookieReadersContext(DbContextOptions<CookieReadersContext> options) : base(options)
        {
        }


        public DbSet<CookieUser> Users { get; set; }
    }
}
