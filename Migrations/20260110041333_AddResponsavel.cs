using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoMidasAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddResponsavel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Responsaveis",
                columns: table => new
                {
                    IdResponsavel = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomeResponsavel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    sobrenomeResponsavel = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    telefoneResponsavel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emailResponsavel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsaveis", x => x.IdResponsavel);
                });

            migrationBuilder.InsertData(
                table: "Responsaveis",
                columns: new[] { "IdResponsavel", "emailResponsavel", "nomeResponsavel", "sobrenomeResponsavel", "telefoneResponsavel" },
                values: new object[] { 1, "joao.silva@teste.com", "João", "Silva", "987654321" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Responsaveis");
        }
    }
}
