using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HITSBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class createBlackListTokenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "blackListTokens",
                columns: table => new
                {
                    userEmail = table.Column<string>(type: "text", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blackListTokens", x => new { x.userEmail, x.Token });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blackListTokens");
        }
    }
}
