using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTDevicesMonitor.Migrations
{
    public partial class AddTimeOnOffOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TimeOffOption",
                table: "LightModules",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TimeOnOption",
                table: "LightModules",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeOffOption",
                table: "LightModules");

            migrationBuilder.DropColumn(
                name: "TimeOnOption",
                table: "LightModules");
        }
    }
}
