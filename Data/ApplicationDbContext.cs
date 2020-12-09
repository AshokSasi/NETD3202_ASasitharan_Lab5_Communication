using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NETD3202_ASasitharan_Lab5_Comm2.Models;

namespace NETD3202_ASasitharan_Lab5_Comm2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Appointment> appointment { get; set; }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<Patient> patients { get; set; }
    }
}
