using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reciclagem.Models.DBORECICLAGEM.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Models.DBORECICLAGEM.Mappers
{
    public class MateriaisMappper : IEntityTypeConfiguration<MateriaisOperacao>
    {
        public void Configure(EntityTypeBuilder<MateriaisOperacao> builder)
        {

            builder.HasOne(c => c.Operacao)
                .WithMany(c => c.MateriaisOperacao)
                .HasForeignKey(c => c.OperacaoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(c => c.Material)
                .WithMany(c => c.MateriaisOperacao)
                .HasForeignKey(c => c.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull);


            builder.Property(c => c.DataCad)
                .HasDefaultValueSql("(getdate())");
        }
    }
}

