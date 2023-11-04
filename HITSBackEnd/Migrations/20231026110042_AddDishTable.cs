using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HITSBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddDishTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_blackListTokens",
                table: "blackListTokens");

            migrationBuilder.RenameTable(
                name: "blackListTokens",
                newName: "BlackListTokens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlackListTokens",
                table: "BlackListTokens",
                columns: new[] { "userEmail", "Token" });

            migrationBuilder.CreateTable(
                name: "Dish",
                columns: table => new
                {
                    Id = table.Column<string>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsVegeterian = table.Column<bool>(type: "boolean", nullable: false),
                    Photo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dish", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlackListTokens",
                table: "BlackListTokens");

            migrationBuilder.RenameTable(
                name: "BlackListTokens",
                newName: "blackListTokens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_blackListTokens",
                table: "blackListTokens",
                columns: new[] { "userEmail", "Token" });
            migrationBuilder.DropTable(
                name: "Dish");
        }
    }
}
