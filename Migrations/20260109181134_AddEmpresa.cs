using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoMidasAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    IdEmpresa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idResponsavel = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    razaoSocial = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    nomeFantasia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    telefoneEmp = table.Column<int>(type: "int", nullable: true),
                    cnpjEmpresa = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    emailEmpresa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.IdEmpresa);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
