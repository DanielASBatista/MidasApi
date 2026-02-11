using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjetoMidasAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRecorrencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoRecorrencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PadraoSistema = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoRecorrencias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recorrencias",
                columns: table => new
                {
                    idRecorrente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProjecao = table.Column<int>(type: "int", nullable: true),
                    TipoLancamento = table.Column<int>(type: "int", nullable: true),
                    TipoRecorrenciaId = table.Column<int>(type: "int", nullable: false),
                    dsRecorrente = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    obRecorrente = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    dataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    qtdeRecorrente = table.Column<int>(type: "int", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    momentoCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioResponsavel = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recorrencias", x => x.idRecorrente);
                    table.ForeignKey(
                        name: "FK_Recorrencias_TipoRecorrencias_TipoRecorrenciaId",
                        column: x => x.TipoRecorrenciaId,
                        principalTable: "TipoRecorrencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TipoRecorrencias",
                columns: new[] { "Id", "Nome", "PadraoSistema" },
                values: new object[,]
                {
                    { 1, "Mensal", true },
                    { 2, "Semanal", true },
                    { 3, "Anual", true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recorrencias_TipoRecorrenciaId",
                table: "Recorrencias",
                column: "TipoRecorrenciaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recorrencias");

            migrationBuilder.DropTable(
                name: "TipoRecorrencias");
        }
    }
}
