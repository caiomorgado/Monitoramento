using CV.Infra.Data.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using Monitoramento.Domain;

namespace Monitoramento.Infra.Data.Context
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<LogModel> Log { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LogConfiguration());
        }
    }
}
