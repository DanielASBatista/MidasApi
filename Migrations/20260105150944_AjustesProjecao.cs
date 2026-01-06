using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoMidasAPI.Migrations
{
    /// <inheritdoc />
    public partial class AjustesProjecao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Lancamentos",
                newName: "DescricaoLancamento");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioResponsavel",
                table: "Projecoes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioResponsavel",
                table: "Projecoes");

            migrationBuilder.RenameColumn(
                name: "DescricaoLancamento",
                table: "Lancamentos",
                newName: "Descricao");
        }
    }
}
