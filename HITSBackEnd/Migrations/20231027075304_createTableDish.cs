using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HITSBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class createTableDish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
    name: "Dishes",
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
        table.PrimaryKey("PK_Dishes", x => x.Id);
    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
