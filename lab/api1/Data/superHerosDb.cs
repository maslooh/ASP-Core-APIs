using api1.Models;

namespace api1.Data
{
    public class superHerosDb:DbContext
    {
        public superHerosDb(DbContextOptions<superHerosDb>options):base(options)
        {

        }
        public virtual DbSet<SuperHero> superHeroes { get; set; }
    }
}
