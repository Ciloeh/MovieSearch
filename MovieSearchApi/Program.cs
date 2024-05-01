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
builder.Services.AddHttpClient();
builder.Services.AddScoped<ISearchQueryService, SearchQueryService>();
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
