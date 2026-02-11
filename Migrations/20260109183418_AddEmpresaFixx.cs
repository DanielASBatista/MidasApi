using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoMidasAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddEmpresaFixx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "telefoneEmp",
                table: "Empresas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Empresas",
                columns: new[] { "IdEmpresa", "cnpjEmpresa", "emailEmpresa", "idResponsavel", "nomeFantasia", "razaoSocial", "telefoneEmp" },
                values: new object[] { 1, "12345678901234", "empresa@teste.com", 1, "Teste", "Empresa Teste", "123456789" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "IdEmpresa",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "telefoneEmp",
                table: "Empresas",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
