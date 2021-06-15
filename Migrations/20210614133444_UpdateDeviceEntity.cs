using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTDevicesMonitor.Migrations
{
    public partial class UpdateDeviceEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HaveLightModule",
                table: "DeviceEntity",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HaveLightModule",
                table: "DeviceEntity");
        }
    }
}
