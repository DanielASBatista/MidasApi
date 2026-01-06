using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoMidasAPI.Migrations
{
    /// <inheritdoc />
    public partial class AjustesModelos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Lancamentos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<int>(
                name: "IdProjecao",
                table: "Lancamentos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObservacaoLancamento",
                table: "Lancamentos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioResponsavel",
                table: "Lancamentos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idRecorrente",
                table: "Lancamentos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idTipoLancamento",
                table: "Lancamentos",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdProjecao",
                table: "Lancamentos");

            migrationBuilder.DropColumn(
                name: "ObservacaoLancamento",
                table: "Lancamentos");

            migrationBuilder.DropColumn(
                name: "UsuarioResponsavel",
                table: "Lancamentos");

            migrationBuilder.DropColumn(
                name: "idRecorrente",
                table: "Lancamentos");

            migrationBuilder.DropColumn(
                name: "idTipoLancamento",
                table: "Lancamentos");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Lancamentos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
