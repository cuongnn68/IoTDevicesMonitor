using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTDevicesMonitor.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Base64Files",
                columns: table => new
                {
                    testname = table.Column<string>(name: "test name", type: "varchar(420)", maxLength: 69, nullable: false),
                    Folder = table.Column<string>(type: "text", nullable: false),
                    File = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Base64Files", x => new { x.testname, x.Folder });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Base64Files");
        }
    }
}
