
using Microsoft.EntityFrameworkCore;

namespace BLL.DAL
{
    public class dataContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public dataContext(DbContextOptions<dataContext> options) : base(options)
        {
        }
    }
}
