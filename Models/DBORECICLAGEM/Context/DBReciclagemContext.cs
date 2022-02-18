using Microsoft.EntityFrameworkCore;
using Reciclagem.Models.DBORECICLAGEM.Entidades;
using Reciclagem.Models.DBORECICLAGEM.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Models.DBORECICLAGEM.Context
{
    public class DBReciclagemContext : DbContext
    {
        public DBReciclagemContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Empresas> Empresas { get; set; }
        public DbSet<Fornecedores> Fornecedores { get; set; }
        public DbSet<Materiais> Materiais { get; set; }
        public DbSet<MateriaisOperacao> MateriaisOperacao { get; set; }
        public DbSet<Operacoes> Operacoes { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<ItemDepesas> ItemDespesas { get; set; }
        public DbSet<Despesas> Despesas { get; set; }
        public DbSet<TipoDespesa> TiposDespesas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmpresaMapper());
            modelBuilder.ApplyConfiguration(new FornecedorMapper());
            modelBuilder.ApplyConfiguration(new MateriaisMapper());
            modelBuilder.ApplyConfiguration(new MateriaisMappper());
            modelBuilder.ApplyConfiguration(new OperacoesMapper());
            modelBuilder.ApplyConfiguration(new UsuarioMapper());
            modelBuilder.ApplyConfiguration(new ItemDespesasMapper());
            modelBuilder.ApplyConfiguration(new DespesasMapper());
            modelBuilder.ApplyConfiguration(new TipoDespesaMapper());
        }
    }
}