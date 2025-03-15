using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanViens_VaiTros_IdVaiTro",
                table: "NhanViens");

            migrationBuilder.DropTable(
                name: "VaiTros");

            migrationBuilder.DropIndex(
                name: "IX_NhanViens_IdVaiTro",
                table: "NhanViens");

            migrationBuilder.DropColumn(
                name: "IdVaiTro",
                table: "NhanViens");

            migrationBuilder.AddColumn<int>(
                name: "VaiTro",
                table: "NhanViens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VaiTro",
                table: "KhachHangs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VaiTro",
                table: "NhanViens");

            migrationBuilder.DropColumn(
                name: "VaiTro",
                table: "KhachHangs");

            migrationBuilder.AddColumn<Guid>(
                name: "IdVaiTro",
                table: "NhanViens",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "VaiTros",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTros", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NhanViens_IdVaiTro",
                table: "NhanViens",
                column: "IdVaiTro");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanViens_VaiTros_IdVaiTro",
                table: "NhanViens",
                column: "IdVaiTro",
                principalTable: "VaiTros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
