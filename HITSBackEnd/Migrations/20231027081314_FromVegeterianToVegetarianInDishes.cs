using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HITSBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class FromVegeterianToVegetarianInDishes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsVegeterian",
                table: "Dishes",
                newName: "IsVegetarian");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
