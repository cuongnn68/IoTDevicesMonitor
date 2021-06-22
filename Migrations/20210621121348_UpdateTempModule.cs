using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTDevicesMonitor.Migrations
{
    public partial class UpdateTempModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LowerAlertOption",
                table: "TempModules",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UpperAlertOption",
                table: "TempModules",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LowerAlertOption",
                table: "TempModules");

            migrationBuilder.DropColumn(
                name: "UpperAlertOption",
                table: "TempModules");
        }
    }
}
