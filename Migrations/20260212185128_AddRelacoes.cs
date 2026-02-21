using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoMidasAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRelacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Recorrencias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Projecoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Emprestimos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 8, 83, 71, 179, 87, 156, 171, 246, 173, 211, 198, 203, 209, 145, 120, 55, 171, 212, 144, 69, 96, 117, 157, 213, 232, 227, 234, 59, 2, 12, 18, 161, 115, 8, 127, 85, 206, 147, 143, 77, 196, 54, 112, 111, 200, 178, 164, 227, 247, 229, 92, 32, 55, 154, 234, 46, 15, 242, 64, 235, 179, 55, 57, 24 }, new byte[] { 37, 4, 227, 181, 42, 210, 12, 53, 43, 6, 166, 178, 105, 122, 37, 130, 120, 77, 179, 227, 172, 150, 194, 131, 70, 133, 184, 52, 143, 172, 52, 45, 184, 7, 36, 203, 23, 41, 9, 167, 98, 135, 196, 175, 219, 15, 88, 61, 195, 251, 204, 69, 11, 93, 107, 1, 51, 38, 178, 129, 182, 190, 37, 160, 88, 172, 143, 252, 175, 136, 52, 161, 193, 86, 127, 152, 67, 230, 146, 249, 139, 34, 84, 127, 182, 171, 55, 22, 51, 207, 106, 49, 193, 55, 9, 35, 188, 79, 200, 174, 222, 132, 163, 35, 190, 111, 131, 228, 195, 233, 240, 3, 151, 105, 162, 120, 41, 231, 137, 249, 68, 152, 15, 175, 79, 150, 142, 133 } });

            migrationBuilder.CreateIndex(
                name: "IX_Recorrencias_IdUsuario",
                table: "Recorrencias",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Projecoes_IdUsuario",
                table: "Projecoes",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimos_IdUsuario",
                table: "Emprestimos",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimos_Usuarios_IdUsuario",
                table: "Emprestimos",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projecoes_Usuarios_IdUsuario",
                table: "Projecoes",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recorrencias_Usuarios_IdUsuario",
                table: "Recorrencias",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimos_Usuarios_IdUsuario",
                table: "Emprestimos");

            migrationBuilder.DropForeignKey(
                name: "FK_Projecoes_Usuarios_IdUsuario",
                table: "Projecoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recorrencias_Usuarios_IdUsuario",
                table: "Recorrencias");

            migrationBuilder.DropIndex(
                name: "IX_Recorrencias_IdUsuario",
                table: "Recorrencias");

            migrationBuilder.DropIndex(
                name: "IX_Projecoes_IdUsuario",
                table: "Projecoes");

            migrationBuilder.DropIndex(
                name: "IX_Emprestimos_IdUsuario",
                table: "Emprestimos");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Recorrencias");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Projecoes");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Emprestimos");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 234, 209, 144, 129, 65, 154, 210, 28, 207, 122, 165, 110, 20, 143, 105, 93, 11, 2, 59, 210, 27, 230, 59, 61, 49, 220, 77, 206, 58, 26, 123, 196, 226, 112, 54, 233, 72, 117, 250, 216, 202, 183, 100, 182, 239, 223, 134, 27, 224, 61, 174, 62, 47, 225, 177, 28, 26, 31, 200, 61, 67, 28, 101, 237 }, new byte[] { 142, 247, 56, 176, 133, 89, 209, 125, 205, 83, 66, 151, 237, 176, 224, 100, 169, 50, 250, 5, 66, 201, 148, 194, 154, 98, 210, 145, 113, 180, 191, 239, 228, 195, 161, 2, 150, 238, 121, 80, 21, 195, 246, 217, 6, 221, 136, 50, 8, 174, 187, 9, 43, 108, 247, 167, 218, 93, 189, 209, 61, 214, 25, 225, 237, 119, 60, 7, 27, 11, 197, 95, 236, 23, 211, 244, 28, 214, 145, 89, 230, 79, 93, 101, 143, 103, 60, 53, 244, 120, 229, 218, 169, 131, 175, 159, 112, 135, 14, 126, 255, 49, 178, 108, 146, 104, 91, 29, 107, 176, 88, 14, 253, 12, 217, 61, 126, 69, 184, 30, 84, 234, 174, 41, 6, 253, 130, 5 } });
        }
    }
}
