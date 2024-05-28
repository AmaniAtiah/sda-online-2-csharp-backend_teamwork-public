using System.Text;
using System.Text.Json.Serialization;
using Backend.EntityFramework;
//using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


DotNetEnv.Env.Load();

var jwtKey = Environment.GetEnvironmentVariable("Jwt__Key") ?? throw new
InvalidOperationException("JWT Key is missing in environment variable.");
var jwtIssuer = Environment.GetEnvironmentVariable("Jwt__Issuer") ?? throw new
InvalidOperationException("JWT Issuer is missing in environment variable.");
var jwtAudience = Environment.GetEnvironmentVariable("Jwt__Audience") ?? throw new
InvalidOperationException("JWT Audience is missing in environment variable.");

var defaultConnection = Environment.GetEnvironmentVariable
("DefaultConnection") ?? throw new InvalidOperationException
("Default Connection is missing in environment variable.");


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ProductService>();
// builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();



    });

});


var Configuration = builder.Configuration;
var key = Encoding.ASCII.GetBytes(jwtKey);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // set this one as a true in production
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = Configuration["Jwt:Issuer"],
        ValidAudience = Configuration["Jwt:Audience"],
        ClockSkew = TimeSpan.Zero
    };
});

Console.WriteLine($"Jwt__Key: {Environment.GetEnvironmentVariable("Jwt__Key")}");

Console.WriteLine($"Jwt__Issuer: {Environment.GetEnvironmentVariable("Jwt__Issuer")}");

Console.WriteLine($"Jwt__Audience: {Environment.GetEnvironmentVariable("Jwt__Audience")}");




builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(defaultConnection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigins");
app.UseHttpsRedirection();
app.MapControllers().WithParameterValidation();
app.Run();
