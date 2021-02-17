using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lisans.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    ProviceId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lisans",
                columns: table => new
                {
                    LisansId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    SchoolName = table.Column<string>(nullable: false),
                    SchoolCode = table.Column<string>(maxLength: 6, nullable: false),
                    HardwareId = table.Column<string>(nullable: false),
                    OnlineProductKey = table.Column<string>(nullable: true),
                    OfflineProductKey = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Status = table.Column<short>(nullable: false),
                    LastOnlineDate = table.Column<DateTime>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    Province = table.Column<int>(nullable: false),
                    District = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lisans", x => x.LisansId);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "Lisans");

            migrationBuilder.DropTable(
                name: "Province");
        }
    }
}
