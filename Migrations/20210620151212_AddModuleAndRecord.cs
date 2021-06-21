using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTDevicesMonitor.Migrations
{
    public partial class AddModuleAndRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HumiModules",
                columns: table => new
                {
                    DeviceId = table.Column<int>(type: "integer", nullable: false),
                    Upperbound = table.Column<int>(type: "integer", nullable: false),
                    Lowerbound = table.Column<int>(type: "integer", nullable: false),
                    Auto = table.Column<bool>(type: "boolean", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HumiModules", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_HumiModules_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HumiRecords",
                columns: table => new
                {
                    DeviceId = table.Column<int>(type: "integer", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HumiRecords", x => new { x.DeviceId, x.Time });
                    table.ForeignKey(
                        name: "FK_HumiRecords_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LightModules",
                columns: table => new
                {
                    DeviceId = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<bool>(type: "boolean", nullable: false),
                    TimeOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TimeOff = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Auto = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LightModules", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_LightModules_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempModules",
                columns: table => new
                {
                    DeviceId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    Upperbound = table.Column<int>(type: "integer", nullable: false),
                    Lowerbound = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempModules", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_TempModules_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempRecords",
                columns: table => new
                {
                    DeviceId = table.Column<int>(type: "integer", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempRecords", x => new { x.DeviceId, x.Time });
                    table.ForeignKey(
                        name: "FK_TempRecords_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HumiModules");

            migrationBuilder.DropTable(
                name: "HumiRecords");

            migrationBuilder.DropTable(
                name: "LightModules");

            migrationBuilder.DropTable(
                name: "TempModules");

            migrationBuilder.DropTable(
                name: "TempRecords");
        }
    }
}
