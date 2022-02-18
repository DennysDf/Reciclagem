using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reciclagem.Models.DBORECICLAGEM.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Models.DBORECICLAGEM.Mappers
{
    public class DespesasMapper : IEntityTypeConfiguration<Despesas>
    {
        public void Configure(EntityTypeBuilder<Despesas> builder)
        {
            builder.HasOne(c => c.Usuario)
                .WithMany(c => c.Despesas)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(c => c.ItemDespesa)
               .WithMany(c => c.Despesas)
               .HasForeignKey(c => c.ItemDespesaId)
               .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(c => c.DataCad)
                .HasDefaultValueSql("(getdate())");

            builder.Property(c => c.IsAtivo)
                .HasDefaultValue(true);

        }
    }
}
