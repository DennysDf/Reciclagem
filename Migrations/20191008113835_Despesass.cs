using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reciclagem.Migrations
{
    public partial class Despesass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Operacoes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Fornecedores",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TiposDespesas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true),
                    Obs = table.Column<string>(nullable: true),
                    IsAtivo = table.Column<bool>(nullable: false, defaultValue: true),
                    DataCad = table.Column<DateTime>(nullable: false, defaultValueSql: "(getdate())"),
                    EmpresaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDespesas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TiposDespesas_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemDespesas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true),
                    TipoDepesaId = table.Column<int>(nullable: false),
                    IsAtivo = table.Column<bool>(nullable: false, defaultValue: true),
                    DataCad = table.Column<DateTime>(nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDespesas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemDespesas_TiposDespesas_TipoDepesaId",
                        column: x => x.TipoDepesaId,
                        principalTable: "TiposDespesas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Despesas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false),
                    Data = table.Column<string>(nullable: true),
                    ItemDespesaId = table.Column<int>(nullable: false),
                    DataCad = table.Column<DateTime>(nullable: false, defaultValueSql: "(getdate())"),
                    IsAtivo = table.Column<bool>(nullable: false, defaultValue: true),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Despesas_ItemDespesas_ItemDespesaId",
                        column: x => x.ItemDespesaId,
                        principalTable: "ItemDespesas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Despesas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_ItemDespesaId",
                table: "Despesas",
                column: "ItemDespesaId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_UsuarioId",
                table: "Despesas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDespesas_TipoDepesaId",
                table: "ItemDespesas",
                column: "TipoDepesaId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposDespesas_EmpresaId",
                table: "TiposDespesas",
                column: "EmpresaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Despesas");

            migrationBuilder.DropTable(
                name: "ItemDespesas");

            migrationBuilder.DropTable(
                name: "TiposDespesas");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Operacoes");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Fornecedores");
        }
    }
}
