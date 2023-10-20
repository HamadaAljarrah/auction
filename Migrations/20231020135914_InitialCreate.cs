using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DistLab2.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuctionDbs",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.AddColumn<int>(
                name: "StartingPrice",
                table: "AuctionDbs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartingPrice",
                table: "AuctionDbs");

            migrationBuilder.InsertData(
                table: "AuctionDbs",
                columns: new[] { "Id", "CreatedDate", "Description", "EndDate", "Name" },
                values: new object[] { -1, new DateTime(2023, 10, 19, 18, 6, 1, 120, DateTimeKind.Local).AddTicks(4088), "test description", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TEST from aucton db dontext" });
        }
    }
}
