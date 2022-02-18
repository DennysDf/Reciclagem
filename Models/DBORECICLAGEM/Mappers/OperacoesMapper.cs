using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reciclagem.Models.DBORECICLAGEM.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Models.DBORECICLAGEM.Mappers
{
    public class OperacoesMapper : IEntityTypeConfiguration<Operacoes>
    {
        public void Configure(EntityTypeBuilder<Operacoes> builder)
        {

            builder.HasOne(c => c.Usuario)
                .WithMany(c => c.Operacoes)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            
            builder.HasOne(c => c.Fornecedor)
                .WithMany(c => c.Operacoes)
                .HasForeignKey(c => c.FornecedorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(c => c.IsAtivo)
                .HasDefaultValue(true);

            builder.Property(c => c.DataCad)
                .HasDefaultValueSql("(getdate())");
        }
    }
}
