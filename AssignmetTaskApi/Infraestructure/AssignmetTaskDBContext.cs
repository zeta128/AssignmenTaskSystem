using AssignmetTaskApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace AssignmetTaskApi.Infraestructure
{
    public class AssignmetTaskDBContext : DbContext
    {
        public AssignmetTaskDBContext(DbContextOptions<AssignmetTaskDBContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(usr =>
            {
                usr.HasKey(col => col.IdUser);
                usr.Property(col => col.IdUser)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

                usr.Property(col => col.FullName).HasMaxLength(60);
                usr.Property(col => col.Email).HasMaxLength(50);
                usr.Property(col => col.Password).HasMaxLength(50);
            });
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
