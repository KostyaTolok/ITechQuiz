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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApplication.Hubs;

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
            services.AddTransient<IAnswersRepository, EFAnswersRepository>();
            services.AddTransient<ICategoriesRepository, EFCategoriesRepository>();
            services.AddTransient<INotificationsRepository, EFNotificationsRepository>();
            services.AddTransient<ISurveysService, SurveysService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IAssignRequestsService, AssignRequestsService>();
            services.AddTransient<IAnswersService, AnswersService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IStatisticsService, StatisticsService>();
            services.AddTransient<INotificationsService, NotificationsService>();

            services.AddDbContext<QuizDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                x => { x.MigrationsAssembly("WebApplication"); }));

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

            services.AddMvc(options => { options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); });

            services.AddAuthentication(opts =>
                {
                    opts.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
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
                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = context =>
                            {
                                var accessToken = context.Request.Query["access_token"];
                                if (!string.IsNullOrEmpty(accessToken))
                                {
                                    context.Token = accessToken;
                                }
                                return Task.CompletedTask;
                            }
                        };
                    });

            services.AddAuthorization();

            services.AddAntiforgery(opts => { opts.HeaderName = "X-XSRF-TOKEN"; });

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });


            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            services.AddSpaStaticFiles(options => { options.RootPath = "client/dist"; });

            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env, ILoggerFactory loggerFactory,
            IAntiforgery antiforgery)
        {
            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(),
                configuration["LogsFileName"]));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ITechQuiz API V1");
                c.DocumentTitle = "ITechQuiz API";
                c.RoutePrefix = "swagger";
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            app.Use(next => context =>
            {
                var path = context.Request.Path.Value;

                if (path != null && path.IndexOf("/api/", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    var tokens = antiforgery.GetAndStoreTokens(context);

                    if (tokens.RequestToken != null)
                        context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
                            new CookieOptions()
                            {
                                HttpOnly = false
                            });
                }

                return next(context);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationHub>("/notify");
            });

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