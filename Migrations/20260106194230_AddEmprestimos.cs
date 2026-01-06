using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoMidasAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddEmprestimos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emprestimos",
                columns: table => new
                {
                    IdSimEmprestimo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomeEmprestimo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descricaoEmprestimo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    provedorEmprestimo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    valorEmprestimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    parcelasEmprestimo = table.Column<int>(type: "int", nullable: false),
                    valorParcelas = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IOFemprestimo = table.Column<decimal>(type: "decimal(5,4)", nullable: false),
                    despesasEmprestimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tarifasEmprestimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataCriacaoSE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioResponsavel = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emprestimos", x => x.IdSimEmprestimo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emprestimos");
        }
    }
}
