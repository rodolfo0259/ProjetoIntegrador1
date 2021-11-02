using ControleCelulasWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleCelulasWebMvc.Data
{
    public class WebDbContext : DbContext
    {
        public DbSet<Coordenador> Coordenadores { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<Celula> Celula { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Reuniao> Reuniao { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>        
            optionsBuilder.UseSqlServer(@"server=(localdb)\mssqllocaldb;database=controlewebmvc;integrated security=true");
    }
}