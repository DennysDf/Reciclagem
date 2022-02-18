using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reciclagem.Models.DBORECICLAGEM.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Models.DBORECICLAGEM.Mappers
{
    public class TipoDespesaMapper : IEntityTypeConfiguration<TipoDespesa>
    {
        public void Configure(EntityTypeBuilder<TipoDespesa> builder)
        {
            builder.HasOne(c => c.Empresa)
                .WithMany(c => c.TipoDespesa)
                .HasForeignKey(c => c.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(c => c.IsAtivo)
            .HasDefaultValue(true);

            builder.Property(c => c.DataCad)
            .HasDefaultValueSql("(getdate())");
        }
    }
}
