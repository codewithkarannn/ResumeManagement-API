using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ResumeManagement_API.Models;
using ResumeManagement_API.Repositories;
using ResumeManagement_API.Services;
using AutoMapper;

using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(typeof(Program));
// JWT Authentication Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
        };
    });

// Add services to the container.
// Register your ICandidate and its implementation
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<ICandidateServices, CandidateService>();
// Register IUserRepository and AuthService
builder.Services.AddScoped<IUserRepository, UserMasterRepository>(); // Register the IUserRepository
builder.Services.AddScoped<IAuthServices, AuthService>(); // Register the IAuthServices

// Add DbContext for database access
builder.Services.AddDbContext<ResumeManagementContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")
        ));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()         // Allows any origin
              .AllowAnyMethod()         // Allows any HTTP method (GET, POST, PUT, DELETE)
              .AllowAnyHeader();        // Allows any headers
    });
});

var app = builder.Build();
// Use CORS middleware
app.UseCors("AllowAllOrigins");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();