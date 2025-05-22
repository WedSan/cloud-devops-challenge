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
    public class DentalHistoryEntityConfig : IEntityTypeConfiguration<DentalHistory>
    {
        public void Configure(EntityTypeBuilder<DentalHistory> builder)
        {
            builder.ToTable("TB_HISTORICO_DENTAL");

            builder.HasKey(e => e.ID);


            builder.Property(e => e.ConsultationDate)
                   .HasColumnName("DATA_CONSULTA");

            builder.Property(e => e.ToothCondition)
                   .HasColumnName("CONDICAO_DENTE");

            builder.HasMany(d => d.Procedures)
                   .WithOne()
                   .HasForeignKey("ID_HISTORICO_DENTAL")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.User)
                   .WithMany()
                   .HasForeignKey("ID_USUARIO")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}