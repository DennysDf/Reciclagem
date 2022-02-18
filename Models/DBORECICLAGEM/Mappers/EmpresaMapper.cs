using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reciclagem.Models.DBORECICLAGEM.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Models.DBORECICLAGEM.Mappers
{
    public class EmpresaMapper : IEntityTypeConfiguration<Empresas>
    {
        public void Configure(EntityTypeBuilder<Empresas> builder)
        {
            builder.Property(c => c.IsAtivo)
              .HasDefaultValue(true);

            builder.Property(c => c.DataCad)
              .HasDefaultValueSql("(getdate())");
        }
    }
}
