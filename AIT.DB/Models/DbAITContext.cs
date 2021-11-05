using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AIT.DB.Models
{
    public partial class DbAITContext : IdentityDbContext<User>
    {
        public DbAITContext()
        {
        }

        public DbAITContext(DbContextOptions<DbAITContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branch> Branchs { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=H-AITD202003005;Initial Catalog=DbAIT;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedingAspNetUser(modelBuilder);
            SeedingAspNetRole(modelBuilder);
            SeedingAspNetUserRole(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
            modelBuilder.HasAnnotation("Relational:Collation", "Japanese_CI_AS");

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.BranchName).HasMaxLength(250);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Avatar).HasMaxLength(500);

                entity.Property(e => e.DateFirst).HasColumnType("date");

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.EmployeeName).HasMaxLength(250);

                entity.Property(e => e.ShortIntro).HasMaxLength(500);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_Employees_Branchs");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.SkillId)
                    .HasConstraintName("FK_Employees_Skills");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.Property(e => e.SkillName).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        private void SeedingAspNetUser(ModelBuilder modelBuilder)
        {
            User user = new User()
            {
                Id = "2c0fca4e-9376-4a70-bcc6-35bebe497866",
                UserName = "Admin",
                Email = "AIT.admin@gmail.com",
                NormalizedEmail = "AIT.admin@gmail.com",
                NormalizedUserName = "AIT.admin@gmail.com",
                LockoutEnabled = false,
                Avatar = "/Images/logo.png"
            };
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            var passwordHash = passwordHasher.HashPassword(user, "Asdf1234!");
            user.PasswordHash = passwordHash;

            modelBuilder.Entity<User>().HasData(user);
        }
        private void SeedingAspNetRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = "c0c6661b-0964-4e62-8083-3cac6a6741ec",
                    Name = "SystemAdmin",
                    NormalizedName = "SystemAdmin",
                    ConcurrencyStamp = "1"
                },
                new IdentityRole()
                {
                    Id = "32ffd287-205f-43a2-9f0d-80sc5309fb47",
                    Name = "Customer",
                    NormalizedName = "Customer",
                    ConcurrencyStamp = "2"
                });
        }
        private void SeedingAspNetUserRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "c0c6661b-0964-4e62-8083-3cac6a6741ec",
                    UserId = "2c0fca4e-9376-4a70-bcc6-35bebe497866"
                });
        }
    }
}
