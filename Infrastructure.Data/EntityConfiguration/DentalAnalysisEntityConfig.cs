using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.EntityConfiguration
{
    public class DentalAnalysisEntityConfig : IEntityTypeConfiguration<DentalAnalysis>
    {
        public void Configure(EntityTypeBuilder<DentalAnalysis> builder)
        {
            builder.ToTable("TB_ANALISE_DENTARIA");

            builder.HasKey(da => da.Id);

            builder.Property(da => da.Id)
                   .HasColumnName("ID");

            builder.Property(da => da.AnalysisDate)
                   .HasColumnName("DATA_ANALISE");

            builder.Property(da => da.ProbabilityProblem)
                   .HasColumnName("PROBABILIDADE_PROBLEMA");

            builder.HasOne(da => da.User)
                   .WithMany()
                   .HasForeignKey("ID_USUARIO")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(da => da.MonitoringDataList)
                .WithMany(md => md.DentalAnalyses)
                .UsingEntity<Dictionary<string, object>>(
                    "TB_ANALISE_DENTARIA_DADO_MONITORAMENTO", 
                    j => j.HasOne<MonitoringData>()
                        .WithMany()
                        .HasForeignKey("ID_DADO_MONITORAMENTO") 
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<DentalAnalysis>()
                        .WithMany()
                        .HasForeignKey("ID_ANALISE_DENTARIA") 
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("ID_ANALISE_DENTARIA", "ID_DADO_MONITORAMENTO");
                    }
                );
        }
    }
}
