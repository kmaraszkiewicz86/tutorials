using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFirstEFApp.Migrations
{
    public partial class add_order_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "BookAuthorId",
                keyValue: 1,
                column: "Order",
                value: (byte)116);

            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "BookAuthorId",
                keyValue: 2,
                column: "Order",
                value: (byte)116);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                column: "PublishedOn",
                value: new DateTime(2020, 2, 18, 22, 4, 59, 892, DateTimeKind.Local).AddTicks(2600));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                column: "PublishedOn",
                value: new DateTime(2020, 2, 18, 22, 4, 59, 896, DateTimeKind.Local).AddTicks(6240));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "BookAuthorId",
                keyValue: 1,
                column: "Order",
                value: (byte)0);

            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "BookAuthorId",
                keyValue: 2,
                column: "Order",
                value: (byte)0);

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
    }
}
