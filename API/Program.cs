using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.AIServices;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Luvia.Infrastructure.Persistence;
// 🔐 JWT
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ===============================
            // Add services to the container
            // ===============================
            builder.Services.AddControllers();

            // ===============================
            // Swagger + JWT Config
            // ===============================
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter 'Bearer YOUR_TOKEN'"
                });

                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            // ===============================
            // DbContext
            // ===============================
            builder.Services.AddDbContext<LuviaDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionWithDB")));
           
            // ===============================
            // Dependency Injection
            // ===============================
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            //builder.Services.AddScoped<IScanService,ScanService>();
            builder.Services.AddScoped<ISmartReportService, SmartReportService>();
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<ITokenBlacklistService, TokenBlacklistService>();

            // ===============================
            // Scan Service
            // ===============================
            builder.Services.AddScoped<IScanService, ScanService>();

            // ===============================
            // 🔐 Authentication (JWT)
            // ===============================
            var jwtKey = builder.Configuration["Jwt:Key"]
                ?? throw new Exception("JWT Key is missing");
            Console.WriteLine("VALIDATION KEY: " + builder.Configuration["Jwt:Key"]);
            var key = Encoding.UTF8.GetBytes(jwtKey);

           

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            ),
            ClockSkew = TimeSpan.Zero,
            //RoleClaimType = "role",
            RoleClaimType = ClaimTypes.Role,
            //RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
            NameClaimType = ClaimTypes.NameIdentifier
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("❌ AUTH FAILED: " + context.Exception.Message);
                return Task.CompletedTask;
            },

            OnTokenValidated = async context =>
            {
                Console.WriteLine("✅ TOKEN VALIDATED");

                var blacklist = context.HttpContext.RequestServices
                    .GetRequiredService<ITokenBlacklistService>();

                var token = context.SecurityToken as System.IdentityModel.Tokens.Jwt.JwtSecurityToken;

                var rawToken = token?.RawData;

                if (!string.IsNullOrEmpty(rawToken))
                {
                    //if (await blacklist.IsTokenRevokedAsync(rawToken))
                    //{
                    //    context.Fail("Token revoked");
                    //}
                }
            }
        };

    });


            builder.Services.AddAuthorization();

            // ===============================
            // Build App
            // ===============================
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LuviaDbContext>();
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            // ===============================
            // 🔥 Seed Users (Admin + Assistant)
            // ===============================
            //using (var scope = app.Services.CreateScope())
            //{
            //    var context = scope.ServiceProvider.GetRequiredService<LuviaDbContext>();
            //    context.Database.EnsureCreated();

            //    if (!context.Users.Any())
            //    {
            //        context.Users.AddRange(
            //            new User
            //            {
            //                Name = "Admin",
            //                Email = "admin@luvia.com",
            //                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
            //                Role = UserRole.Admin,
            //                IsActive = true
            //            },
            //            new User
            //            {
            //                Name = "Assistant",
            //                Email = "assistant@luvia.com",
            //                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
            //                Role = UserRole.Assistant,
            //                IsActive = true
            //            }
            //        );

            //        context.SaveChanges();
            //    }
            //}

            // ===============================
            // Middleware
            // ===============================
           
                app.UseSwagger();
                app.UseSwaggerUI();
            

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}