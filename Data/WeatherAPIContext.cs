using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class WeatherAPIContext : DbContext
    {
        public WeatherAPIContext (DbContextOptions<WeatherAPIContext> options)
            : base(options)
        {
        }

        public DbSet<City> City { get; set; }
    }
}
