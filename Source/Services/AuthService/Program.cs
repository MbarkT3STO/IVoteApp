var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add Identity
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
	options.Password.RequireDigit              = false;
	options.Password.RequireLowercase          = false;
	options.Password.RequireNonAlphanumeric    = false;
	options.Password.RequireUppercase          = false;
	options.Password.RequiredLength            = 6;
	options.Password.RequiredUniqueChars       = 0;
	options.SignIn.RequireConfirmedAccount     = false;
	options.SignIn.RequireConfirmedEmail       = false;
	options.SignIn.RequireConfirmedPhoneNumber = false;
	options.User.RequireUniqueEmail            = true;
}).AddRoles<AppRole>().AddEntityFrameworkStores<AppDbContext>();

// Configure JWT
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddOptions<JwtSettings>().Bind(builder.Configuration.GetSection("JwtSettings"));

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
		ValidateIssuer               = true,
		ValidateAudience             = true,
		ValidateLifetime             = true,
		ValidateIssuerSigningKey     = true,
		ValidIssuer                  = issuer,
		ValidAudience                = audience,
		IssuerSigningKey             = symmetricKey,
		ClockSkew                    = TimeSpan.Zero
	};
});

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddControllers();




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
