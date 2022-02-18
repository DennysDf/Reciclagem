﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Reciclagem.Models.DBORECICLAGEM.Context;

namespace Reciclagem.Migrations
{
    [DbContext(typeof(DBReciclagemContext))]
    partial class DBReciclagemContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Reciclagem.Models.DBORECICLAGEM.Entidades.Despesas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Data");

                    b.Property<DateTime>("DataCad")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Descricao");

                    b.Property<bool>("IsAtivo")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<int>("ItemDespesaId");

                    b.Property<int>("UsuarioId");

                    b.Property<decimal>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("ItemDespesaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Despesas");
                });

            modelBuilder.Entity("Reciclagem.Models.DBORECICLAGEM.Entidades.Empresas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCad")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Endereco");

                    b.Property<bool>("IsAtivo")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("Reciclagem.Models.DBORECICLAGEM.Entidades.Fornecedores", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cidade");

                    b.Property<DateTime>("DataCad")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Endereco");

                    b.Property<bool>("IsAtivo")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Nome");

                    b.Property<string>("Telefone");

                    b.Property<int>("Tipo");

                    b.HasKey("Id");

                    b.ToTable("Fornecedores");
                });

            modelBuilder.Entity("Reciclagem.Models.DBORECICLAGEM.Entidades.ItemDepesas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCad")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Descricao");

                    b.Property<bool>("IsAtivo")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<int>("TipoDepesaId");

                    b.HasKey("Id");

                    b.HasIndex("TipoDepesaId");

                    b.ToTable("ItemDespesas");
                });

            modelBuilder.Entity("Reciclagem.Models.DBORECICLAGEM.Entidades.Materiais", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCad")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Descricao");

                    b.Property<bool>("IsAtivo")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.HasKey("Id");

                    b.ToTable("Materiais");
                });

            modelBuilder.Entity("Reciclagem.Models.DBORECICLAGEM.Entidades.MateriaisOperacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCad")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("MaterialId");

                    b.Property<int>("OperacaoId");

                    b.Property<decimal>("Peso");

                    b.Property<decimal>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("OperacaoId");

                    b.ToTable("MateriaisOperacao");
                });

            modelBuilder.Entity("Reciclagem.Models.DBORECICLAGEM.Entidades.Operacoes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Data");

                    b.Property<DateTime>("DataCad")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("FornecedorId");

                    b.Property<bool>("IsAtivo")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<int?>("Status");

                    b.Property<int>("Tipo");

                    b.Property<int>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Operacoes");
                });

            modelBuilder.Entity("Reciclagem.Models.DBORECICLAGEM.Entidades.TipoDespesa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCad")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Descricao");

                    b.Property<int>("EmpresaId");

                    b.Property<bool>("IsAtivo")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Obs");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("TiposDespesas");
                });

            modelBuilder.Entity("Reciclagem.Models.DBORECICLAGEM.Entidades.Usuarios", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCad")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Email");

                    b.Property<int>("EmpresaId");

                    b.Property<string>("Foto");

                    b.Property<bool>("IsAtivo")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<DateTime?>("LastAcesso");

                    b.Property<string>("Login");

                    b.Property<string>("Nome");

                    b.Property<string>("Senha");

                    b.Property<int>("Tipo");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Reciclagem.Models.DBORECICLAGEM.Entidades.Despesas", b =>
                {
                    b.HasOne("Reciclagem.Models.DBORECICLAGEM.Entidades.ItemDepesas", "ItemDespesa")
                        .WithMany("Despesas")
                        .HasForeignKey("ItemDespesaId");

                    b.HasOne("Reciclagem.Models.DBORECICLAGEM.Entidades.Usuarios", "Usuario")
                        .WithMany("Despesas")
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("Reciclagem.Models.DBORECICLAGEM.Entidades.ItemDepesas", b =>
                {
                    b.HasOne("Reciclagem.Models.DBORECICLAGEM.Entidades.TipoDespesa", "TipoDespesa")
                        .WithMany("ItemDespesa")
                        .HasForeignKey("TipoDepesaId");
                });

            modelBuilder.Entity("Reciclagem.Models.DBORECICLAGEM.Entidades.MateriaisOperacao", b =>
                {
                    b.HasOne("Reciclagem.Models.DBORECICLAGEM.Entidades.Materiais", "Material")
                        .WithMany("MateriaisOperacao")
                        .HasForeignKey("MaterialId");

                    b.HasOne("Reciclagem.Models.DBORECICLAGEM.Entidades.Operacoes", "Operacao")
                        .WithMany("MateriaisOperacao")
                        .HasForeignKey("OperacaoId");
                });

            modelBuilder.Entity("Reciclagem.Models.DBORECICLAGEM.Entidades.Operacoes", b =>
                {
                    b.HasOne("Reciclagem.Models.DBORECICLAGEM.Entidades.Fornecedores", "Fornecedor")
                        .WithMany("Operacoes")
                        .HasForeignKey("FornecedorId");

                    b.HasOne("Reciclagem.Models.DBORECICLAGEM.Entidades.Usuarios", "Usuario")
                        .WithMany("Operacoes")
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("Reciclagem.Models.DBORECICLAGEM.Entidades.TipoDespesa", b =>
                {
                    b.HasOne("Reciclagem.Models.DBORECICLAGEM.Entidades.Empresas", "Empresa")
                        .WithMany("TipoDespesa")
                        .HasForeignKey("EmpresaId");
                });

            modelBuilder.Entity("Reciclagem.Models.DBORECICLAGEM.Entidades.Usuarios", b =>
                {
                    b.HasOne("Reciclagem.Models.DBORECICLAGEM.Entidades.Empresas", "Empresa")
                        .WithMany("Usuarios")
                        .HasForeignKey("EmpresaId");
                });
#pragma warning restore 612, 618
        }
    }
}
