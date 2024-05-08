using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProtracV1.Migrations
{
    /// <inheritdoc />
    public partial class InputRegister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InputRegister",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SerialNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectTitle = table.Column<string>(type: "TEXT", nullable: true),
                    DataReceived = table.Column<string>(type: "TEXT", nullable: true),
                    ReceivedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReceivedFrom = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectManagerName = table.Column<string>(type: "TEXT", nullable: true),
                    FilesFormat = table.Column<string>(type: "TEXT", nullable: true),
                    NumberofFiles = table.Column<int>(type: "INTEGER", nullable: false),
                    FitforPurpose = table.Column<bool>(type: "INTEGER", nullable: false),
                    Check = table.Column<bool>(type: "INTEGER", nullable: false),
                    CheckedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CheckedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Custodian = table.Column<string>(type: "TEXT", nullable: true),
                    StoragePath = table.Column<string>(type: "TEXT", nullable: true),
                    Remarks = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputRegister", x => x.ProjectId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InputRegister");
        }
    }
}
