using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
 
namespace dojoactivity.Models
{
    public class DojoContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public DojoContext(DbContextOptions<DojoContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Planner> Activities { get; set;}
        public DbSet<Participant> Participants { get; set; }
    }
}
