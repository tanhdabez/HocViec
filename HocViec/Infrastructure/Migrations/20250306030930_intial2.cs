using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class intial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDon_ChiTietSanPham_ChiTietSanPhamId",
                table: "ChiTietHoaDon");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDon_HoaDon_HoaDonId",
                table: "ChiTietHoaDon");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietSanPham_SanPham_SanPhamId",
                table: "ChiTietSanPham");

            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_NhanVien_NhanVienId",
                table: "HoaDon");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_VaiTro_VaiTroId",
                table: "NhanVien");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPham_DanhMucLoaiHang_DanhMucLoaiHangId",
                table: "SanPham");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPham_NhaCungCap_NhaCungCapId",
                table: "SanPham");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VaiTro",
                table: "VaiTro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SanPham",
                table: "SanPham");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NhanVien",
                table: "NhanVien");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NhaCungCap",
                table: "NhaCungCap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KhachHang",
                table: "KhachHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HoaDon",
                table: "HoaDon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DanhMucLoaiHang",
                table: "DanhMucLoaiHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietSanPham",
                table: "ChiTietSanPham");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietHoaDon",
                table: "ChiTietHoaDon");

            migrationBuilder.RenameTable(
                name: "VaiTro",
                newName: "VaiTros");

            migrationBuilder.RenameTable(
                name: "SanPham",
                newName: "SanPhams");

            migrationBuilder.RenameTable(
                name: "NhanVien",
                newName: "NhanViens");

            migrationBuilder.RenameTable(
                name: "NhaCungCap",
                newName: "NhaCungCaps");

            migrationBuilder.RenameTable(
                name: "KhachHang",
                newName: "KhachHangs");

            migrationBuilder.RenameTable(
                name: "HoaDon",
                newName: "HoaDons");

            migrationBuilder.RenameTable(
                name: "DanhMucLoaiHang",
                newName: "DanhMucLoaiHangs");

            migrationBuilder.RenameTable(
                name: "ChiTietSanPham",
                newName: "ChiTietSanPhams");

            migrationBuilder.RenameTable(
                name: "ChiTietHoaDon",
                newName: "ChiTietHoaDons");

            migrationBuilder.RenameIndex(
                name: "IX_SanPham_NhaCungCapId",
                table: "SanPhams",
                newName: "IX_SanPhams_NhaCungCapId");

            migrationBuilder.RenameIndex(
                name: "IX_SanPham_DanhMucLoaiHangId",
                table: "SanPhams",
                newName: "IX_SanPhams_DanhMucLoaiHangId");

            migrationBuilder.RenameIndex(
                name: "IX_NhanVien_VaiTroId",
                table: "NhanViens",
                newName: "IX_NhanViens_VaiTroId");

            migrationBuilder.RenameIndex(
                name: "IX_HoaDon_NhanVienId",
                table: "HoaDons",
                newName: "IX_HoaDons_NhanVienId");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietSanPham_SanPhamId",
                table: "ChiTietSanPhams",
                newName: "IX_ChiTietSanPhams_SanPhamId");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietHoaDon_HoaDonId",
                table: "ChiTietHoaDons",
                newName: "IX_ChiTietHoaDons_HoaDonId");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietHoaDon_ChiTietSanPhamId",
                table: "ChiTietHoaDons",
                newName: "IX_ChiTietHoaDons_ChiTietSanPhamId");

            migrationBuilder.AlterColumn<bool>(
                name: "TrangThai",
                table: "VaiTros",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "NhaCungCapId",
                table: "SanPhams",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "DanhMucLoaiHangId",
                table: "SanPhams",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<bool>(
                name: "TrangThai",
                table: "NhanViens",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "NhanViens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "KhachHangs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VaiTros",
                table: "VaiTros",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SanPhams",
                table: "SanPhams",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NhanViens",
                table: "NhanViens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NhaCungCaps",
                table: "NhaCungCaps",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KhachHangs",
                table: "KhachHangs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HoaDons",
                table: "HoaDons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DanhMucLoaiHangs",
                table: "DanhMucLoaiHangs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietSanPhams",
                table: "ChiTietSanPhams",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietHoaDons",
                table: "ChiTietHoaDons",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_VaiTros",
                table: "VaiTros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SanPhams",
                table: "SanPhams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NhanViens",
                table: "NhanViens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NhaCungCaps",
                table: "NhaCungCaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KhachHangs",
                table: "KhachHangs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HoaDons",
                table: "HoaDons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DanhMucLoaiHangs",
                table: "DanhMucLoaiHangs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietSanPhams",
                table: "ChiTietSanPhams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietHoaDons",
                table: "ChiTietHoaDons");

            migrationBuilder.RenameTable(
                name: "VaiTros",
                newName: "VaiTro");

            migrationBuilder.RenameTable(
                name: "SanPhams",
                newName: "SanPham");

            migrationBuilder.RenameTable(
                name: "NhanViens",
                newName: "NhanVien");

            migrationBuilder.RenameTable(
                name: "NhaCungCaps",
                newName: "NhaCungCap");

            migrationBuilder.RenameTable(
                name: "KhachHangs",
                newName: "KhachHang");

            migrationBuilder.RenameTable(
                name: "HoaDons",
                newName: "HoaDon");

            migrationBuilder.RenameTable(
                name: "DanhMucLoaiHangs",
                newName: "DanhMucLoaiHang");

            migrationBuilder.RenameTable(
                name: "ChiTietSanPhams",
                newName: "ChiTietSanPham");

            migrationBuilder.RenameTable(
                name: "ChiTietHoaDons",
                newName: "ChiTietHoaDon");

            migrationBuilder.RenameIndex(
                name: "IX_SanPhams_NhaCungCapId",
                table: "SanPham",
                newName: "IX_SanPham_NhaCungCapId");

            migrationBuilder.RenameIndex(
                name: "IX_SanPhams_DanhMucLoaiHangId",
                table: "SanPham",
                newName: "IX_SanPham_DanhMucLoaiHangId");

            migrationBuilder.RenameIndex(
                name: "IX_NhanViens_VaiTroId",
                table: "NhanVien",
                newName: "IX_NhanVien_VaiTroId");

            migrationBuilder.RenameIndex(
                name: "IX_HoaDons_NhanVienId",
                table: "HoaDon",
                newName: "IX_HoaDon_NhanVienId");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietSanPhams_SanPhamId",
                table: "ChiTietSanPham",
                newName: "IX_ChiTietSanPham_SanPhamId");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietHoaDons_HoaDonId",
                table: "ChiTietHoaDon",
                newName: "IX_ChiTietHoaDon_HoaDonId");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietHoaDons_ChiTietSanPhamId",
                table: "ChiTietHoaDon",
                newName: "IX_ChiTietHoaDon_ChiTietSanPhamId");

            migrationBuilder.AlterColumn<int>(
                name: "TrangThai",
                table: "VaiTro",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<Guid>(
                name: "NhaCungCapId",
                table: "SanPham",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DanhMucLoaiHangId",
                table: "SanPham",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TrangThai",
                table: "NhanVien",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "NhanVien",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "KhachHang",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VaiTro",
                table: "VaiTro",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SanPham",
                table: "SanPham",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NhanVien",
                table: "NhanVien",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NhaCungCap",
                table: "NhaCungCap",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KhachHang",
                table: "KhachHang",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HoaDon",
                table: "HoaDon",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DanhMucLoaiHang",
                table: "DanhMucLoaiHang",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietSanPham",
                table: "ChiTietSanPham",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietHoaDon",
                table: "ChiTietHoaDon",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDon_ChiTietSanPham_ChiTietSanPhamId",
                table: "ChiTietHoaDon",
                column: "ChiTietSanPhamId",
                principalTable: "ChiTietSanPham",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDon_HoaDon_HoaDonId",
                table: "ChiTietHoaDon",
                column: "HoaDonId",
                principalTable: "HoaDon",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietSanPham_SanPham_SanPhamId",
                table: "ChiTietSanPham",
                column: "SanPhamId",
                principalTable: "SanPham",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_NhanVien_NhanVienId",
                table: "HoaDon",
                column: "NhanVienId",
                principalTable: "NhanVien",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_VaiTro_VaiTroId",
                table: "NhanVien",
                column: "VaiTroId",
                principalTable: "VaiTro",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SanPham_DanhMucLoaiHang_DanhMucLoaiHangId",
                table: "SanPham",
                column: "DanhMucLoaiHangId",
                principalTable: "DanhMucLoaiHang",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPham_NhaCungCap_NhaCungCapId",
                table: "SanPham",
                column: "NhaCungCapId",
                principalTable: "NhaCungCap",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
