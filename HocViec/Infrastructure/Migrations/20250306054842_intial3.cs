using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class intial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDons_ChiTietSanPhams_ChiTietSanPhamId",
                table: "ChiTietHoaDons");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDons_HoaDons_HoaDonId",
                table: "ChiTietHoaDons");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietSanPhams_SanPhams_SanPhamId",
                table: "ChiTietSanPhams");

            migrationBuilder.DropForeignKey(
                name: "FK_HoaDons_NhanViens_NhanVienId",
                table: "HoaDons");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanViens_VaiTros_VaiTroId",
                table: "NhanViens");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhams_DanhMucLoaiHangs_DanhMucLoaiHangId",
                table: "SanPhams");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhams_NhaCungCaps_NhaCungCapId",
                table: "SanPhams");

            migrationBuilder.DropIndex(
                name: "IX_SanPhams_DanhMucLoaiHangId",
                table: "SanPhams");

            migrationBuilder.DropIndex(
                name: "IX_SanPhams_NhaCungCapId",
                table: "SanPhams");

            migrationBuilder.DropIndex(
                name: "IX_NhanViens_VaiTroId",
                table: "NhanViens");

            migrationBuilder.DropIndex(
                name: "IX_HoaDons_NhanVienId",
                table: "HoaDons");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietSanPhams_SanPhamId",
                table: "ChiTietSanPhams");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietHoaDons_ChiTietSanPhamId",
                table: "ChiTietHoaDons");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietHoaDons_HoaDonId",
                table: "ChiTietHoaDons");

            migrationBuilder.DropColumn(
                name: "DanhMucLoaiHangId",
                table: "SanPhams");

            migrationBuilder.DropColumn(
                name: "NhaCungCapId",
                table: "SanPhams");

            migrationBuilder.DropColumn(
                name: "VaiTroId",
                table: "NhanViens");

            migrationBuilder.DropColumn(
                name: "NhanVienId",
                table: "HoaDons");

            migrationBuilder.DropColumn(
                name: "SanPhamId",
                table: "ChiTietSanPhams");

            migrationBuilder.DropColumn(
                name: "ChiTietSanPhamId",
                table: "ChiTietHoaDons");

            migrationBuilder.DropColumn(
                name: "HoaDonId",
                table: "ChiTietHoaDons");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_IdDanhMucSanPham",
                table: "SanPhams",
                column: "IdDanhMucSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_IdNhaCungCap",
                table: "SanPhams",
                column: "IdNhaCungCap");

            migrationBuilder.CreateIndex(
                name: "IX_NhanViens_IdVaiTro",
                table: "NhanViens",
                column: "IdVaiTro");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_IdNhanVien",
                table: "HoaDons",
                column: "IdNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPhams_IdSanPham",
                table: "ChiTietSanPhams",
                column: "IdSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDons_IdCTSP",
                table: "ChiTietHoaDons",
                column: "IdCTSP");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDons_IDHoaDon",
                table: "ChiTietHoaDons",
                column: "IDHoaDon");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDons_ChiTietSanPhams_IdCTSP",
                table: "ChiTietHoaDons",
                column: "IdCTSP",
                principalTable: "ChiTietSanPhams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDons_HoaDons_IDHoaDon",
                table: "ChiTietHoaDons",
                column: "IDHoaDon",
                principalTable: "HoaDons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietSanPhams_SanPhams_IdSanPham",
                table: "ChiTietSanPhams",
                column: "IdSanPham",
                principalTable: "SanPhams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDons_NhanViens_IdNhanVien",
                table: "HoaDons",
                column: "IdNhanVien",
                principalTable: "NhanViens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanViens_VaiTros_IdVaiTro",
                table: "NhanViens",
                column: "IdVaiTro",
                principalTable: "VaiTros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhams_DanhMucLoaiHangs_IdDanhMucSanPham",
                table: "SanPhams",
                column: "IdDanhMucSanPham",
                principalTable: "DanhMucLoaiHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhams_NhaCungCaps_IdNhaCungCap",
                table: "SanPhams",
                column: "IdNhaCungCap",
                principalTable: "NhaCungCaps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDons_ChiTietSanPhams_IdCTSP",
                table: "ChiTietHoaDons");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDons_HoaDons_IDHoaDon",
                table: "ChiTietHoaDons");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietSanPhams_SanPhams_IdSanPham",
                table: "ChiTietSanPhams");

            migrationBuilder.DropForeignKey(
                name: "FK_HoaDons_NhanViens_IdNhanVien",
                table: "HoaDons");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanViens_VaiTros_IdVaiTro",
                table: "NhanViens");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhams_DanhMucLoaiHangs_IdDanhMucSanPham",
                table: "SanPhams");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhams_NhaCungCaps_IdNhaCungCap",
                table: "SanPhams");

            migrationBuilder.DropIndex(
                name: "IX_SanPhams_IdDanhMucSanPham",
                table: "SanPhams");

            migrationBuilder.DropIndex(
                name: "IX_SanPhams_IdNhaCungCap",
                table: "SanPhams");

            migrationBuilder.DropIndex(
                name: "IX_NhanViens_IdVaiTro",
                table: "NhanViens");

            migrationBuilder.DropIndex(
                name: "IX_HoaDons_IdNhanVien",
                table: "HoaDons");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietSanPhams_IdSanPham",
                table: "ChiTietSanPhams");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietHoaDons_IdCTSP",
                table: "ChiTietHoaDons");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietHoaDons_IDHoaDon",
                table: "ChiTietHoaDons");

            migrationBuilder.AddColumn<Guid>(
                name: "DanhMucLoaiHangId",
                table: "SanPhams",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NhaCungCapId",
                table: "SanPhams",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VaiTroId",
                table: "NhanViens",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NhanVienId",
                table: "HoaDons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SanPhamId",
                table: "ChiTietSanPhams",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ChiTietSanPhamId",
                table: "ChiTietHoaDons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HoaDonId",
                table: "ChiTietHoaDons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_DanhMucLoaiHangId",
                table: "SanPhams",
                column: "DanhMucLoaiHangId");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_NhaCungCapId",
                table: "SanPhams",
                column: "NhaCungCapId");

            migrationBuilder.CreateIndex(
                name: "IX_NhanViens_VaiTroId",
                table: "NhanViens",
                column: "VaiTroId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_NhanVienId",
                table: "HoaDons",
                column: "NhanVienId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPhams_SanPhamId",
                table: "ChiTietSanPhams",
                column: "SanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDons_ChiTietSanPhamId",
                table: "ChiTietHoaDons",
                column: "ChiTietSanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDons_HoaDonId",
                table: "ChiTietHoaDons",
                column: "HoaDonId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDons_ChiTietSanPhams_ChiTietSanPhamId",
                table: "ChiTietHoaDons",
                column: "ChiTietSanPhamId",
                principalTable: "ChiTietSanPhams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDons_HoaDons_HoaDonId",
                table: "ChiTietHoaDons",
                column: "HoaDonId",
                principalTable: "HoaDons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietSanPhams_SanPhams_SanPhamId",
                table: "ChiTietSanPhams",
                column: "SanPhamId",
                principalTable: "SanPhams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDons_NhanViens_NhanVienId",
                table: "HoaDons",
                column: "NhanVienId",
                principalTable: "NhanViens",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanViens_VaiTros_VaiTroId",
                table: "NhanViens",
                column: "VaiTroId",
                principalTable: "VaiTros",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhams_DanhMucLoaiHangs_DanhMucLoaiHangId",
                table: "SanPhams",
                column: "DanhMucLoaiHangId",
                principalTable: "DanhMucLoaiHangs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhams_NhaCungCaps_NhaCungCapId",
                table: "SanPhams",
                column: "NhaCungCapId",
                principalTable: "NhaCungCaps",
                principalColumn: "Id");
        }
    }
}
