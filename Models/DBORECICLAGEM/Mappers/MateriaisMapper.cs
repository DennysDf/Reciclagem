using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reciclagem.Models.DBORECICLAGEM.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Models.DBORECICLAGEM.Mappers
{
    public class MateriaisMapper : IEntityTypeConfiguration<Materiais>
    {
        public void Configure(EntityTypeBuilder<Materiais> builder)
        {
            builder.Property(c => c.IsAtivo)
              .HasDefaultValue(true);

            builder.Property(c => c.DataCad)
              .HasDefaultValueSql("(getdate())");
        }
    }
}
