using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkRelationshipsTesting.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DogOwners",
                columns: table => new
                {
                    DogOwnerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DogOwners", x => x.DogOwnerId);
                });

            migrationBuilder.CreateTable(
                name: "Dogs",
                columns: table => new
                {
                    DogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dogs", x => x.DogId);
                });

            migrationBuilder.CreateTable(
                name: "DogBreeders",
                columns: table => new
                {
                    DogBreederId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    DogId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DogBreeders", x => x.DogBreederId);
                    table.ForeignKey(
                        name: "FK_DogBreeders_Dogs_DogId",
                        column: x => x.DogId,
                        principalTable: "Dogs",
                        principalColumn: "DogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DogOwnerDogs",
                columns: table => new
                {
                    DogId = table.Column<int>(nullable: false),
                    DogOwnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DogOwnerDogs", x => new { x.DogId, x.DogOwnerId });
                    table.ForeignKey(
                        name: "FK_DogOwnerDogs_Dogs_DogId",
                        column: x => x.DogId,
                        principalTable: "Dogs",
                        principalColumn: "DogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DogOwnerDogs_DogOwners_DogOwnerId",
                        column: x => x.DogOwnerId,
                        principalTable: "DogOwners",
                        principalColumn: "DogOwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Puppies",
                columns: table => new
                {
                    PuppyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    DogId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puppies", x => x.PuppyId);
                    table.ForeignKey(
                        name: "FK_Puppies_Dogs_DogId",
                        column: x => x.DogId,
                        principalTable: "Dogs",
                        principalColumn: "DogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Dogs",
                columns: new[] { "DogId", "Name" },
                values: new object[,]
                {
                    { 1, "Mailo" },
                    { 2, "Lilo" },
                    { 3, "Szarlo" },
                    { 4, "Izka" }
                });

            migrationBuilder.InsertData(
                table: "Puppies",
                columns: new[] { "PuppyId", "DogId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Mailo1" },
                    { 2, 1, "Mailo2" },
                    { 3, 2, "Lilo1" },
                    { 4, 2, "Lilo2" },
                    { 5, 2, "Lilo3" },
                    { 6, 3, "Szarlo1" },
                    { 7, 3, "Szarlo2" },
                    { 8, 3, "Szarlo3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DogBreeders_DogId",
                table: "DogBreeders",
                column: "DogId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DogOwnerDogs_DogOwnerId",
                table: "DogOwnerDogs",
                column: "DogOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Puppies_DogId",
                table: "Puppies",
                column: "DogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DogBreeders");

            migrationBuilder.DropTable(
                name: "DogOwnerDogs");

            migrationBuilder.DropTable(
                name: "Puppies");

            migrationBuilder.DropTable(
                name: "DogOwners");

            migrationBuilder.DropTable(
                name: "Dogs");
        }
    }
}
