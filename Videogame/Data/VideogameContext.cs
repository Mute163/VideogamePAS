using Microsoft.EntityFrameworkCore;
using Videogame.Models;

namespace Videogame.Data
{
    public class VideogameContext : DbContext
    {
        public VideogameContext(DbContextOptions<VideogameContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Player { get; set; }

    }
}