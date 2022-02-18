using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reciclagem.Models.DBORECICLAGEM.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Models.DBORECICLAGEM.Mappers
{
    public class UsuarioMapper : IEntityTypeConfiguration<Usuarios>
    {
        public void Configure(EntityTypeBuilder<Usuarios> builder)
        {

            builder.HasOne(c => c.Empresa)
            .WithMany(c => c.Usuarios)
            .HasForeignKey(c => c.EmpresaId)
            .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(c => c.IsAtivo)
                .HasDefaultValue(true);

            builder.Property(c => c.DataCad)
                .HasDefaultValueSql("(getdate())");
        }
    }
}
