using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProtracV1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckReviewForm",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SerialNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectTitle = table.Column<string>(type: "TEXT", nullable: true),
                    ActivityNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    ActivityName = table.Column<string>(type: "TEXT", nullable: true),
                    Objectives = table.Column<string>(type: "TEXT", nullable: true),
                    ReferenceStandards = table.Column<string>(type: "TEXT", nullable: true),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    SpecificQualityIssues = table.Column<string>(type: "TEXT", nullable: true),
                    Completion = table.Column<bool>(type: "INTEGER", nullable: false),
                    CheckedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CheckedByDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ApprovedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ApprovedByDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ActionTaken = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckReviewForm", x => x.ProjectId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckReviewForm");
        }
    }
}
