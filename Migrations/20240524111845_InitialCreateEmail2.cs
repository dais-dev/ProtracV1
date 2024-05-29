using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProtracV1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateEmail2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailSettings",
                columns: table => new
                {
                    EmailId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MailServer = table.Column<string>(type: "TEXT", nullable: false),
                    MailPort = table.Column<int>(type: "INTEGER", nullable: false),
                    SenderName = table.Column<string>(type: "TEXT", nullable: false),
                    Sender = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSettings", x => x.EmailId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailSettings");
        }
    }
}
