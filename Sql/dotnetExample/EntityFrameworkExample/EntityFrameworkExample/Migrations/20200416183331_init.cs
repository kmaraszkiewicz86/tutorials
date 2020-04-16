using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkExample.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CustomerId = table.Column<int>(nullable: true),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offers_Offers_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Kevin Costner" },
                    { 2, "Akshay Kumar" },
                    { 3, "Sean Connery" },
                    { 4, "Sanjay Dutt" },
                    { 5, "Sharukh Khan" },
                    { 6, "Lilo Liloviskow" },
                    { 7, "Mailo Mailovich" },
                    { 8, "Szarko Szarkovich" },
                    { 9, "Izka Mariskovich" }
                });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "CustomerId", "Name", "ParentId" },
                values: new object[] { 1, null, "Offer1", null });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "CustomerId", "Name", "ParentId" },
                values: new object[,]
                {
                    { 2, 1, "Offer2", null },
                    { 3, 2, "Offer3", null },
                    { 4, 5, "Offer4", null },
                    { 5, 6, "Offer4", null },
                    { 6, 7, "Offer4", null },
                    { 7, 8, "Offer4", null },
                    { 11, 8, "Offer4", null },
                    { 12, 8, "Offer4", null },
                    { 13, 8, "Offer4", null },
                    { 14, 8, "Offer4", null },
                    { 8, null, "Offer1.1", 1 }
                });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "CustomerId", "Name", "ParentId" },
                values: new object[] { 9, null, "Offer2.1", 2 });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "CustomerId", "Name", "ParentId" },
                values: new object[] { 10, null, "Offer3.1", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CustomerId",
                table: "Offers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_ParentId",
                table: "Offers",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
