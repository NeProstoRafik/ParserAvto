using Microsoft.EntityFrameworkCore;
using ParserAvto.Models;

namespace ParserAvto.DAL
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
            //  Database.EnsureDeleted();
            Database.EnsureCreated();

        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Avto> Avtos { get; set; }
    }
}
