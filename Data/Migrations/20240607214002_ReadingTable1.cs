using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DayHospital.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReadingTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PatientId",
                table: "ExtraReadings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "ExtraReadings");
        }
    }
}
