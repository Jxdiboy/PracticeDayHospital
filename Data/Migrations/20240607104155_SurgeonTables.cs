using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DayHospital.Data.Migrations
{
    /// <inheritdoc />
    public partial class SurgeonTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ContraIndications");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ContraIndications");

            migrationBuilder.AlterColumn<string>(
                name: "PharamID",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ActiveIngredientId",
                table: "ContraIndications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CondtionID",
                table: "ContraIndications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Dosages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dosages", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dosages");

            migrationBuilder.DropColumn(
                name: "ActiveIngredientId",
                table: "ContraIndications");

            migrationBuilder.DropColumn(
                name: "CondtionID",
                table: "ContraIndications");

            migrationBuilder.AlterColumn<string>(
                name: "PharamID",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ContraIndications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ContraIndications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
