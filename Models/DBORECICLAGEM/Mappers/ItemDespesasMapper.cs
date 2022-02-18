using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reciclagem.Models.DBORECICLAGEM.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Models.DBORECICLAGEM.Mappers
{
    public class ItemDespesasMapper : IEntityTypeConfiguration<ItemDepesas>
    {
        public void Configure(EntityTypeBuilder<ItemDepesas> builder)
        {

            builder.HasOne(c => c.TipoDespesa)
               .WithMany(c => c.ItemDespesa)
               .HasForeignKey(c => c.TipoDepesaId)
               .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(c => c.DataCad)
                .HasDefaultValueSql("(getdate())");

            builder.Property(c => c.IsAtivo)
                .HasDefaultValue(true);

        }
    }
}
