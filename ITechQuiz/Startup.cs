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
using System.Text;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.IdentityModel.Tokens;

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

            services.AddDbContext<QuizDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                x =>
                {
                    x.MigrationsAssembly("WebApplication");
                }));

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
            var a = configuration["Token:Issuer"];
            services.AddAuthentication()
                .AddCookie().AddJwtBearer(
                    options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidIssuer = configuration["Token:Issuer"],
                            ValidateAudience = true,
                            ValidAudience = configuration["Token:Audience"],
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey
                                (Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                            ValidateLifetime = true
                        };
                    });
            
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

            services.AddSpaStaticFiles(options =>
            {
                options.RootPath = "client/dist";
            });
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
                c.RoutePrefix = "swagger";
            });
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client";
 
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
            
        }
    }
}