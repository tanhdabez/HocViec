using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDons_ChiTietSanPhams_IdCTSP",
                table: "ChiTietHoaDons");

            migrationBuilder.DropTable(
                name: "ChiTietSanPhams");

            migrationBuilder.RenameColumn(
                name: "IdCTSP",
                table: "ChiTietHoaDons",
                newName: "IdSanPham");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietHoaDons_IdCTSP",
                table: "ChiTietHoaDons",
                newName: "IX_ChiTietHoaDons_IdSanPham");

            migrationBuilder.AddColumn<int>(
                name: "GiaBan",
                table: "SanPhams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoLuong",
                table: "SanPhams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDons_SanPhams_IdSanPham",
                table: "ChiTietHoaDons",
                column: "IdSanPham",
                principalTable: "SanPhams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDons_SanPhams_IdSanPham",
                table: "ChiTietHoaDons");

            migrationBuilder.DropColumn(
                name: "GiaBan",
                table: "SanPhams");

            migrationBuilder.DropColumn(
                name: "SoLuong",
                table: "SanPhams");

            migrationBuilder.RenameColumn(
                name: "IdSanPham",
                table: "ChiTietHoaDons",
                newName: "IdCTSP");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietHoaDons_IdSanPham",
                table: "ChiTietHoaDons",
                newName: "IX_ChiTietHoaDons_IdCTSP");

            migrationBuilder.CreateTable(
                name: "ChiTietSanPhams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GiaBan = table.Column<int>(type: "int", nullable: false),
                    MaChiTietSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietSanPhams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietSanPhams_SanPhams_IdSanPham",
                        column: x => x.IdSanPham,
                        principalTable: "SanPhams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPhams_IdSanPham",
                table: "ChiTietSanPhams",
                column: "IdSanPham");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDons_ChiTietSanPhams_IdCTSP",
                table: "ChiTietHoaDons",
                column: "IdCTSP",
                principalTable: "ChiTietSanPhams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
