using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.Extensions.Logging;
using System.IO;
using Application.Interfaces.Repositories;
using Infrastructure.Data.Repositories;
using Infrastructure.Services;
using Application.Interfaces.Services;
using Infrastructure.Data;
using Domain.Entities.Auth;
using System;

namespace WebApplication
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISurveysRepository, EFSurveysRepository>();
            services.AddTransient<IQuestionsRepository, EFQuestionsRepository>();
            services.AddTransient<IOptionsRepository, EFOptionsRepository>();
            services.AddTransient<IAssignRequestsRepository, EFAssignRequestsRepository>();
            services.AddTransient<ISurveysService, SurveysService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IAssignRequestsService, AssignRequestsService>();

            services.AddDbContext<QuizDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly("WebApplication")));

            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                options.Lockout.MaxFailedAccessAttempts = 3;
            }).AddEntityFrameworkStores<QuizDbContext>().AddSignInManager<SignInManager<User>>();

            var assembly = AppDomain.CurrentDomain.Load("Infrastructure");
            services.AddMediatR(assembly);
            services.AddAutoMapper(assembly);

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            })
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);

            services.AddMvcCore();

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(),
                configuration["LogsFileName"]));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ITechQuiz API V1");
                c.DocumentTitle = "ITechQuiz API";
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
