using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DayHospital.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_tables1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionMedicines_Prescriptions_PrescriptionId",
                table: "PrescriptionMedicines");

            migrationBuilder.AddColumn<string>(
                name: "PharamID",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "PrescriptionId",
                table: "PrescriptionMedicines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionMedicines_Prescriptions_PrescriptionId",
                table: "PrescriptionMedicines",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionMedicines_Prescriptions_PrescriptionId",
                table: "PrescriptionMedicines");

            migrationBuilder.DropColumn(
                name: "PharamID",
                table: "Prescriptions");

            migrationBuilder.AlterColumn<int>(
                name: "PrescriptionId",
                table: "PrescriptionMedicines",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionMedicines_Prescriptions_PrescriptionId",
                table: "PrescriptionMedicines",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id");
        }
    }
}
