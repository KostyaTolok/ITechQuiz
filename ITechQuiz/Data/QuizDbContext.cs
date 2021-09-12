using ITechQuiz.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ITechQuiz.Data
{
    [ExcludeFromCodeCoverage]
    public class QuizDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Survey> Surveys { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Option> Options { get; set; }

        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Role>().HasData(new Role()
            {
                Id = new Guid("fb96bb35-90fd-4f70-99a0-954fcfb14baf"),
                Name = "admin",
                NormalizedName = "ADMIN",
                Description = "Управляет добавлением и удалением клиентов"
            }, new Role()
            {
                Id = new Guid("f7d36113-51ff-4b07-8b5f-64fccc8091d5"),
                Name = "client",
                NormalizedName = "CLIENT",
                Description = "Создает, удаляет и изменяет опросы. Имеет доступ к статистике "
            });

            builder.Entity<User>().HasData(new User()
            {
                Id = new Guid("d91046a9-d12b-4c14-9810-ac3af195066a"),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "admin")
            }, new User()
            {
                Id = new Guid("52f4c7c6-7f95-4d40-8308-b36a3ce86a52"),
                UserName = "client",
                NormalizedUserName = "CLIENT",
                Email = "client@gmail.com",
                NormalizedEmail = "CLIENT@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "client")
            });

            builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>()
            {
                RoleId = new Guid("fb96bb35-90fd-4f70-99a0-954fcfb14baf"),
                UserId = new Guid("d91046a9-d12b-4c14-9810-ac3af195066a")
            }, new IdentityUserRole<Guid>()
            {
                RoleId = new Guid("f7d36113-51ff-4b07-8b5f-64fccc8091d5"),
                UserId = new Guid("52f4c7c6-7f95-4d40-8308-b36a3ce86a52")
            });
        }
    }
}
