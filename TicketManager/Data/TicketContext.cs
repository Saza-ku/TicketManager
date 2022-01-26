using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TicketManager.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManager.Data
{
    public class TicketContext : IdentityDbContext
    {
        public DbSet<DramaModel> Dramas { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<MemberResercationModel> MemberReservations { get; set; }
        public DbSet<OutsideReservation> OutsideReservations { get; set; }
        public DbSet<NotifiedMember> NotifiedMemberIds { get; set; }

        public TicketContext(DbContextOptions option)
            : base(option)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Stage>()
                .HasKey(s => new { s.DramaName, s.Num });
        }
    }
}
