using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkExample.Migrations
{
    public partial class view : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "CREATE VIEW View_CustomerOfferFullJoin " +
                "AS " +
                "SELECT " +
                "c.Id as CustomerId, " +
                "c.Name as Customer, " +
                "o.Id AS OfferId, " +
                "o.Name AS Offer " +
                "FROM " +
                "dbo.Customers AS c " +
                "FULL OUTER JOIN " +
                "dbo.Offers AS o ON c.Id = o.CustomerId");

            migrationBuilder.Sql(
                "CREATE VIEW View_CustomerOfferRightJoin " +
                    "AS " +
                    "SELECT " +
                        "c.Id as CustomerId, " +
                        "c.Name as Customer, " +
                        "o.Id AS OfferId, " +
                        "o.Name AS Offer " +
                    "FROM " +
                        "dbo.Customers AS c " +
                    "RIGHT OUTER JOIN " +
                        "dbo.Offers AS o ON c.Id = o.CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "DROP VIEW IF EXISTS View_CustomerOfferFullJoin");

            migrationBuilder.Sql(
                "DROP VIEW IF EXISTS View_CustomerOfferRightJoin");
        }
    }
}
