using ITechQuiz.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITechQuiz.Data
{
    public class QuizDbContext : IdentityDbContext<IdentityUser>
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

            builder.Entity<IdentityRole>().HasData(new IdentityRole()
            {
                Id = "fb96bb35-90fd-4f70-99a0-954fcfb14baf",
                Name = "admin",
                NormalizedName = "ADMIN"
            }, new IdentityRole()
            {
                Id = "f7d36113-51ff-4b07-8b5f-64fccc8091d5",
                Name = "client",
                NormalizedName = "CLIENT"
            }, new IdentityRole()
            {
                Id = "2c408920-31d7-45a5-8f8a-a473f5760d85",
                Name = "vip",
                NormalizedName = "VIP"
            });

            builder.Entity<IdentityUser>().HasData(new IdentityUser()
            {
                Id = "d91046a9-d12b-4c14-9810-ac3af195066a",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "admin")
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>()
            {
                RoleId = "fb96bb35-90fd-4f70-99a0-954fcfb14baf",
                UserId = "d91046a9-d12b-4c14-9810-ac3af195066a"
            });
        }
    }
}
