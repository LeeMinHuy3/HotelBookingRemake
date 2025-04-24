using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_HotelBooking.Migrations
{
    /// <inheritdoc />
    public partial class dataseeddichvu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "DichVus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "DichVus",
                keyColumn: "MaDV",
                keyValue: 1,
                column: "ImageUrl",
                value: "images/dichvu_vip.jpg");

            migrationBuilder.UpdateData(
                table: "DichVus",
                keyColumn: "MaDV",
                keyValue: 2,
                column: "ImageUrl",
                value: "images/dichvu_dondep.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "DichVus");
        }
    }
}
