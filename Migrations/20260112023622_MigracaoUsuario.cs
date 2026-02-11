using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoMidasAPI.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioResponsavel",
                table: "Lancamentos");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Usuarios",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Usuarios",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Perfil",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "Visitante");

            migrationBuilder.AddColumn<string>(
                name: "nomeUsuario",
                table: "Usuarios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Lancamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1,
                columns: new[] { "IdEmpresa", "PasswordHash", "PasswordSalt", "Perfil", "TipoUsuario", "emailUsuario", "nome", "nomeUsuario", "sobrenome", "telefone" },
                values: new object[] { 0, new byte[] { 69, 168, 91, 91, 195, 220, 168, 215, 187, 218, 234, 145, 250, 172, 186, 39, 18, 164, 107, 250, 133, 163, 221, 73, 28, 14, 119, 4, 133, 54, 147, 235, 49, 184, 88, 79, 6, 230, 11, 84, 93, 126, 150, 94, 203, 8, 187, 192, 112, 190, 181, 95, 33, 208, 155, 0, 121, 91, 39, 20, 140, 110, 177, 190 }, new byte[] { 156, 150, 67, 155, 242, 25, 80, 39, 65, 156, 162, 85, 109, 108, 192, 228, 74, 30, 229, 27, 86, 14, 220, 156, 197, 29, 241, 137, 125, 26, 0, 73, 223, 147, 35, 163, 221, 245, 138, 150, 0, 189, 63, 86, 8, 191, 4, 86, 214, 136, 190, 214, 45, 2, 153, 73, 248, 46, 97, 9, 147, 106, 1, 193, 245, 123, 55, 114, 141, 117, 23, 84, 192, 189, 6, 142, 101, 34, 43, 126, 246, 183, 102, 68, 12, 124, 201, 247, 23, 230, 157, 32, 49, 246, 93, 83, 234, 111, 4, 186, 47, 40, 205, 110, 1, 215, 66, 128, 64, 110, 209, 118, 167, 228, 178, 36, 65, 53, 199, 152, 160, 99, 168, 208, 190, 54, 109, 195 }, "Administrador", 7, "", "", "Admin", "", "" });

            migrationBuilder.CreateIndex(
                name: "IX_Lancamentos_IdUsuario",
                table: "Lancamentos",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Lancamentos_Usuarios_IdUsuario",
                table: "Lancamentos",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lancamentos_Usuarios_IdUsuario",
                table: "Lancamentos");

            migrationBuilder.DropIndex(
                name: "IX_Lancamentos_IdUsuario",
                table: "Lancamentos");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Perfil",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "nomeUsuario",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Lancamentos");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioResponsavel",
                table: "Lancamentos",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1,
                columns: new[] { "IdEmpresa", "TipoUsuario", "emailUsuario", "nome", "sobrenome", "telefone" },
                values: new object[] { 1, 2, "maria.oliveira@teste.com", "Maria", "Oliveira", "987654321" });
        }
    }
}
