using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class GalerijaContext : DbContext
    {
        public DbSet<Galerija> Galerije {get; set;}
        public DbSet<Dostupno> GalerijaUmetnici {get; set;}
        public DbSet<Umetnik> Umetnici {get; set;}

        public DbSet<UmetnickoDelo> UmetnickaDela { get; set; }

        public DbSet<Izlozba> Izlozbe { get; set; }

         public DbSet<Karta> Karte { get; set; }

        public DbSet<Izlozeno> DelaIzlozbe { get; set; }

        public GalerijaContext(DbContextOptions options) : base(options)
        {

        }
    }
        
}