using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using BudgetManagementSystemNew.Repositories;
using BudgetManagementSystemNew.Services;
using SecureAuthApi.Helpers;
using BudgetManagementSystemNew.DataAccess;
using ExpenseManagementAPI.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Get connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register MySqlConnection as a scoped service
builder.Services.AddScoped<MySqlConnection>(_ => new MySqlConnection(connectionString));

// Enable CORS for frontend communication
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Ensure correct frontend URL
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Enable Session Handling
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add controllers
builder.Services.AddControllers();

// Register repositories
builder.Services.AddScoped<RoleRepository>(_ => new RoleRepository(connectionString));
builder.Services.AddScoped<DepartmentRepository>(_ => new DepartmentRepository(connectionString));
builder.Services.AddScoped<UserRepository>(_ => new UserRepository(connectionString));
builder.Services.AddScoped<ViewExpenseRepository>(_ => new ViewExpenseRepository(connectionString));
builder.Services.AddScoped<AddExpenseRepository>(_ => new AddExpenseRepository(connectionString));

// Register services
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<JwtHelper>();
builder.Services.AddScoped<SecureAuthApi.Services.ResetPasswordService>();
builder.Services.AddScoped<ViewExpenseService>();
builder.Services.AddScoped<AddExpenseService>();

// JWT Authentication Configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Add Swagger for API Documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Budget Management API",
        Version = "v1",
        Description = "API for managing budget and expenses in the system."
    });

    // Enable JWT Authentication in Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer {your token}'",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
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
            new string[] { }
        }
    });
});

var app = builder.Build();

// Configure Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Exception Handling Middleware
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(new { Message = "An unexpected error occurred.", Error = ex.Message });
    }
});

// Enable Session & CORS
app.UseSession();
app.UseCors("AllowReactApp");

// Enable Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Map Controllers
app.MapControllers();

// Example API Endpoint for Testing DB Connection
app.MapGet("/test-db", async (MySqlConnection connection) =>
{
    await connection.OpenAsync();
    var command = new MySqlCommand("SELECT NOW();", connection);
    var result = await command.ExecuteScalarAsync();
    return Results.Ok(new { DatabaseTime = result });
});

// Run the Application
app.Run();
