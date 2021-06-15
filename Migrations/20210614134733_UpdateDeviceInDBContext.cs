using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTDevicesMonitor.Migrations
{
    public partial class UpdateDeviceInDBContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceEntity_User_Username",
                table: "DeviceEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeviceEntity",
                table: "DeviceEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdminAccount",
                table: "AdminAccount");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "DeviceEntity",
                newName: "Devices");

            migrationBuilder.RenameTable(
                name: "AdminAccount",
                newName: "AdminAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_DeviceEntity_Username",
                table: "Devices",
                newName: "IX_Devices_Username");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Username");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Devices",
                table: "Devices",
                column: "DeviceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdminAccounts",
                table: "AdminAccounts",
                column: "Admin");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Users_Username",
                table: "Devices",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Users_Username",
                table: "Devices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Devices",
                table: "Devices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdminAccounts",
                table: "AdminAccounts");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Devices",
                newName: "DeviceEntity");

            migrationBuilder.RenameTable(
                name: "AdminAccounts",
                newName: "AdminAccount");

            migrationBuilder.RenameIndex(
                name: "IX_Devices_Username",
                table: "DeviceEntity",
                newName: "IX_DeviceEntity_Username");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Username");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeviceEntity",
                table: "DeviceEntity",
                column: "DeviceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdminAccount",
                table: "AdminAccount",
                column: "Admin");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceEntity_User_Username",
                table: "DeviceEntity",
                column: "Username",
                principalTable: "User",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
