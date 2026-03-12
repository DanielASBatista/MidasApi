using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoMidasAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNewConnectionString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recorrencias_TipoRecorrencias_TipoRecorrenciaId",
                table: "Recorrencias");

            migrationBuilder.AlterColumn<int>(
                name: "TipoRecorrenciaId",
                table: "Recorrencias",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdTipoRecorrencia",
                table: "Recorrencias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 183, 170, 168, 227, 1, 11, 230, 24, 134, 215, 51, 247, 209, 32, 224, 254, 196, 195, 212, 227, 35, 225, 43, 152, 204, 122, 160, 184, 234, 55, 125, 40, 6, 124, 118, 222, 146, 239, 9, 216, 187, 213, 133, 36, 64, 126, 192, 51, 218, 115, 62, 8, 184, 246, 247, 143, 80, 72, 218, 82, 244, 125, 81, 89 }, new byte[] { 38, 251, 59, 210, 202, 4, 220, 64, 80, 81, 138, 214, 186, 222, 15, 120, 92, 31, 208, 95, 87, 223, 211, 180, 251, 82, 89, 65, 188, 248, 49, 98, 64, 86, 213, 252, 157, 189, 142, 245, 236, 180, 91, 101, 26, 20, 35, 65, 151, 249, 206, 28, 93, 9, 40, 15, 74, 95, 35, 87, 232, 224, 241, 57, 156, 122, 174, 33, 5, 179, 201, 49, 170, 130, 139, 194, 51, 19, 128, 70, 169, 31, 157, 87, 177, 205, 210, 246, 176, 97, 72, 93, 242, 42, 1, 90, 163, 172, 190, 5, 194, 227, 85, 149, 65, 217, 175, 168, 38, 83, 112, 168, 111, 100, 152, 34, 244, 163, 35, 211, 159, 100, 173, 231, 177, 181, 165, 71 } });

            migrationBuilder.AddForeignKey(
                name: "FK_Recorrencias_TipoRecorrencias_TipoRecorrenciaId",
                table: "Recorrencias",
                column: "TipoRecorrenciaId",
                principalTable: "TipoRecorrencias",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recorrencias_TipoRecorrencias_TipoRecorrenciaId",
                table: "Recorrencias");

            migrationBuilder.DropColumn(
                name: "IdTipoRecorrencia",
                table: "Recorrencias");

            migrationBuilder.AlterColumn<int>(
                name: "TipoRecorrenciaId",
                table: "Recorrencias",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 209, 48, 216, 129, 92, 192, 64, 85, 160, 106, 252, 28, 219, 15, 172, 147, 67, 53, 112, 192, 242, 172, 141, 218, 132, 196, 186, 147, 71, 67, 226, 209, 216, 144, 254, 142, 11, 201, 205, 16, 112, 83, 79, 18, 173, 211, 243, 180, 148, 169, 208, 118, 245, 54, 22, 26, 55, 201, 252, 39, 78, 41, 44, 175 }, new byte[] { 87, 235, 151, 241, 56, 115, 242, 85, 42, 54, 4, 9, 99, 175, 221, 0, 174, 101, 84, 136, 140, 227, 199, 8, 110, 90, 128, 165, 28, 135, 140, 39, 68, 211, 32, 123, 50, 170, 150, 185, 223, 34, 177, 25, 14, 160, 3, 34, 191, 20, 14, 169, 8, 31, 188, 40, 235, 130, 188, 154, 118, 50, 3, 10, 13, 27, 113, 66, 59, 28, 219, 66, 62, 90, 74, 117, 248, 10, 220, 14, 149, 227, 251, 207, 90, 9, 187, 136, 10, 37, 44, 211, 20, 134, 105, 176, 194, 59, 45, 137, 37, 31, 140, 157, 91, 195, 115, 195, 190, 82, 10, 168, 128, 44, 200, 109, 124, 14, 56, 47, 149, 196, 13, 194, 29, 136, 155, 113 } });

            migrationBuilder.AddForeignKey(
                name: "FK_Recorrencias_TipoRecorrencias_TipoRecorrenciaId",
                table: "Recorrencias",
                column: "TipoRecorrenciaId",
                principalTable: "TipoRecorrencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
