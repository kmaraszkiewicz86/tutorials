using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFirstEFApp.Migrations
{
    public partial class alter_add_column_order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Order",
                table: "BookAuthors",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                column: "PublishedOn",
                value: new DateTime(2020, 2, 18, 22, 1, 56, 500, DateTimeKind.Local).AddTicks(7160));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                column: "PublishedOn",
                value: new DateTime(2020, 2, 18, 22, 1, 56, 505, DateTimeKind.Local).AddTicks(2600));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "BookAuthors");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                column: "PublishedOn",
                value: new DateTime(2020, 2, 18, 21, 24, 0, 168, DateTimeKind.Local).AddTicks(7370));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                column: "PublishedOn",
                value: new DateTime(2020, 2, 18, 21, 24, 0, 172, DateTimeKind.Local).AddTicks(9150));
        }
    }
}
