using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HITSBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    BirthData = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Users");
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Users",
                newName: "PhoneNumber");
            migrationBuilder.RenameColumn(
                name: "BirthData",
                table: "Users",
                newName: "BirthDate");
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
