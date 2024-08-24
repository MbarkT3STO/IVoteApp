using System.Text;
using ElectionService.CQRS.DI;
using ElectionService.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add EF core
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Application services
builder.Services.AddApplicationServices();
builder.Services.AddSqlServerCache(builder.Configuration);

// RabbitMQ registration
builder.Services.ConfigureRabbitMQ(builder.Configuration);

// Register the Message Consumers
builder.Services.RegisterMessageConsumers();


// Add Authentication
var issuer       = builder.Configuration["JwtSettings:Issuer"];
var audience     = builder.Configuration["JwtSettings:Audience"];
var key          = builder.Configuration["JwtSettings:Key"];
var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer           = true,
		ValidateAudience         = true,
		ValidateLifetime         = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer              = issuer,
		ValidAudience            = audience,
		IssuerSigningKey         = symmetricKey,
		ClockSkew                = TimeSpan.Zero
	};
});




builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configure CORS
builder.Services.AddCors( options =>
{
	options.AddPolicy("AllowSpecificOrigin",
		builder =>
		{
			builder.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyMethod();
		});
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

