using AppointmentSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AppointmentSystemAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Можеш додати Fluent API-конфігурацію тут, якщо потрібно
        }
    }
}
