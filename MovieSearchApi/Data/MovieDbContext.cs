using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieSearchApi.Model;


namespace MovieSearchApi.Data
{
    public class MovieDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public MovieDbContext(DbContextOptions<MovieDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<MoviesDeatils> Movies { get; set; }
        public DbSet<SearchQuery> SearchQueries { get; set; }
        public DbSet<Users> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
        }
    }
}
