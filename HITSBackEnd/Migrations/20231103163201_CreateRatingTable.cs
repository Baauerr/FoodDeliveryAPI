using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HITSBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class CreateRatingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DishesRating",
                columns: table => new
                {
                    DishId = table.Column<string>(type: "text", nullable: false),
                    UserEmail = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishesRating", x => new { x.UserEmail, x.DishId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DishesRating");
        }
    }
}
