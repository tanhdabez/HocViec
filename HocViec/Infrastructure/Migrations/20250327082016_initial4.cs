using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "ChiTietHoaDons");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "AnhSanPhams");

            migrationBuilder.AlterColumn<int>(
                name: "TrangThai",
                table: "HoaDons",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "TrangThai",
                table: "HoaDons",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai",
                table: "ChiTietHoaDons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai",
                table: "Carts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai",
                table: "AnhSanPhams",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
