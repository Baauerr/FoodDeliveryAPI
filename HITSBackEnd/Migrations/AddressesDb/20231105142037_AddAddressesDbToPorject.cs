using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HITSBackEnd.Migrations.AddressesDb
{
    /// <inheritdoc />
    public partial class AddAddressesDbToPorject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AsAddrObjects",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    objectid = table.Column<long>(type: "bigint", nullable: false),
                    objectguid = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    typename = table.Column<string>(type: "text", nullable: false),
                    level = table.Column<string>(type: "text", nullable: false),
                    isactive = table.Column<int>(type: "integer", nullable: false),
                    isactual = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsAddrObjects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AsAdmHierarchy",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    objectid = table.Column<long>(type: "bigint", nullable: false),
                    parentobjId = table.Column<long>(type: "bigint", nullable: false),
                    isactive = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsAdmHierarchy", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AsHouses",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    objectid = table.Column<long>(type: "bigint", nullable: false),
                    objectguid = table.Column<Guid>(type: "uuid", nullable: false),
                    housetype = table.Column<int>(type: "integer", nullable: false),
                    housenum = table.Column<string>(type: "text", nullable: false),
                    isactual = table.Column<int>(type: "integer", nullable: false),
                    isactive = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsHouses", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AsAddrObjects");

            migrationBuilder.DropTable(
                name: "AsAdmHierarchy");

            migrationBuilder.DropTable(
                name: "AsHouses");
        }
    }
}
