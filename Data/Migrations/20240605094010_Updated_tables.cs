using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DayHospital.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionMedicines_Reasons_ReasonsId",
                table: "PrescriptionMedicines");

            migrationBuilder.DropTable(
                name: "Reasons");

            migrationBuilder.DropIndex(
                name: "IX_PrescriptionMedicines_ReasonsId",
                table: "PrescriptionMedicines");

            migrationBuilder.DropColumn(
                name: "ReasonsId",
                table: "PrescriptionMedicines");

            migrationBuilder.DropColumn(
                name: "ContraIndicationId",
                table: "MedicationIndications");

            migrationBuilder.DropColumn(
                name: "IndicationId",
                table: "MedicationContraIndications");

            migrationBuilder.AddColumn<string>(
                name: "PharmacistId",
                table: "PrescriptionMedicines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PharmacistReason",
                table: "PrescriptionMedicines",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PharmacistId",
                table: "PrescriptionMedicines");

            migrationBuilder.DropColumn(
                name: "PharmacistReason",
                table: "PrescriptionMedicines");

            migrationBuilder.AddColumn<int>(
                name: "ReasonsId",
                table: "PrescriptionMedicines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContraIndicationId",
                table: "MedicationIndications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IndicationId",
                table: "MedicationContraIndications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Reasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacistId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PharmacistReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresMedicineId = table.Column<int>(type: "int", nullable: true),
                    SurgeonReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reasons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionMedicines_ReasonsId",
                table: "PrescriptionMedicines",
                column: "ReasonsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionMedicines_Reasons_ReasonsId",
                table: "PrescriptionMedicines",
                column: "ReasonsId",
                principalTable: "Reasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
