using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monitoramento.Domain;

namespace CV.Infra.Data.EntitiesConfiguration
{
    public class LogConfiguration : IEntityTypeConfiguration<LogModel>
    {
        public void Configure(EntityTypeBuilder<LogModel> builder)
        {
            builder.ToTable("Log");
        }
    }
}
