using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoMidasAPI.Migrations
{
    /// <inheritdoc />
    public partial class Ajesto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nome",
                table: "Usuarios");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 234, 209, 144, 129, 65, 154, 210, 28, 207, 122, 165, 110, 20, 143, 105, 93, 11, 2, 59, 210, 27, 230, 59, 61, 49, 220, 77, 206, 58, 26, 123, 196, 226, 112, 54, 233, 72, 117, 250, 216, 202, 183, 100, 182, 239, 223, 134, 27, 224, 61, 174, 62, 47, 225, 177, 28, 26, 31, 200, 61, 67, 28, 101, 237 }, new byte[] { 142, 247, 56, 176, 133, 89, 209, 125, 205, 83, 66, 151, 237, 176, 224, 100, 169, 50, 250, 5, 66, 201, 148, 194, 154, 98, 210, 145, 113, 180, 191, 239, 228, 195, 161, 2, 150, 238, 121, 80, 21, 195, 246, 217, 6, 221, 136, 50, 8, 174, 187, 9, 43, 108, 247, 167, 218, 93, 189, 209, 61, 214, 25, 225, 237, 119, 60, 7, 27, 11, 197, 95, 236, 23, 211, 244, 28, 214, 145, 89, 230, 79, 93, 101, 143, 103, 60, 53, 244, 120, 229, 218, 169, 131, 175, 159, 112, 135, 14, 126, 255, 49, 178, 108, 146, 104, 91, 29, 107, 176, 88, 14, 253, 12, 217, 61, 126, 69, 184, 30, 84, 234, 174, 41, 6, 253, 130, 5 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "nome",
                table: "Usuarios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt", "nome" },
                values: new object[] { new byte[] { 133, 240, 134, 59, 90, 28, 94, 86, 21, 179, 210, 63, 154, 113, 84, 227, 250, 239, 143, 134, 30, 32, 188, 22, 143, 123, 89, 254, 233, 165, 156, 191, 119, 234, 72, 75, 159, 34, 197, 168, 135, 116, 53, 61, 94, 26, 38, 27, 241, 153, 247, 158, 163, 78, 95, 253, 122, 137, 90, 185, 69, 120, 110, 57 }, new byte[] { 145, 144, 142, 236, 70, 223, 243, 11, 86, 63, 69, 60, 193, 171, 224, 90, 184, 172, 31, 129, 15, 25, 128, 156, 158, 26, 54, 197, 53, 245, 136, 123, 98, 200, 219, 212, 116, 198, 192, 85, 168, 102, 168, 231, 15, 104, 161, 103, 10, 167, 85, 67, 81, 131, 101, 27, 103, 92, 38, 219, 171, 205, 171, 232, 218, 12, 241, 244, 113, 218, 179, 94, 168, 173, 171, 149, 24, 50, 223, 140, 175, 106, 226, 170, 244, 201, 184, 10, 217, 203, 172, 159, 142, 202, 116, 204, 61, 107, 137, 29, 235, 161, 104, 166, 10, 146, 155, 6, 125, 182, 224, 202, 230, 46, 83, 175, 62, 252, 167, 202, 61, 164, 147, 229, 48, 13, 139, 191 }, "" });
        }
    }
}
