using Microsoft.EntityFrameworkCore;
using MovieSearchApi.Data;
using MovieSearchApi.Services;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MovieDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))));
builder.Services.Configure<OmdbApiSettings>(builder.Configuration.GetSection("OmdbApi"));
builder.Services.AddScoped<IMovieService, MovieServices>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ISearchQueryService, SearchQueryService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Configuration that sets all base part

builder.Configuration
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
	.AddEnvironmentVariables();


builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy",
		builder => builder
			.WithOrigins("http://localhost:3000") // Removed trailing slash
			.AllowAnyMethod()
			.AllowAnyHeader()
			.AllowCredentials());
});

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
app.UseCors("CorsPolicy");
app.Run();
