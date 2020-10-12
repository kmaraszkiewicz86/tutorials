using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkRelationshipsTesting.Migrations
{
    public partial class view_and_function : Migration
    {
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(@"CREATE FUNCTION [dbo].[GetAllPuppies](@DogName NVARCHAR(50))
                RETURNS TABLE 
                AS

	                RETURN SELECT 
		                p.Name
	                FROM 
		                Puppies AS p
	                JOIN
		                Dogs as d ON P.DogId = d.DogId
	                WHERE
		        d.Name = @DogName;");

			migrationBuilder.Sql(@"CREATE FUNCTION [dbo].[GetFirstDogPuppy](@DogId INT)
				RETURNS NVARCHAR(50)
				AS
				BEGIN
					DECLARE @DogName NVARCHAR(50);

					SELECT 
						TOP(1) 
						@DogName = p.[Name] 
					 FROM 
						Dogs AS d 
						JOIN Puppies AS p ON d.DogId = p.DogId 
					 WHERE
						d.[DogId] = @DogId

					RETURN @DogName;
				END;");

			migrationBuilder.Sql(@"CREATE VIEW dbo.GetPupiesWithParentDogName
				AS
				SELECT 
					d.[Name] as ParentDogName,
					p.[Name] as PuppyName
				FROM
					Dogs AS d
					LEFT JOIN Puppies AS p ON d.DogId = p.DogId;");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("DROP FUNCTION IF EXISTS  [dbo].[GetAllPuppies];");
			migrationBuilder.Sql("DROP FUNCTION IF EXISTS [dbo].[GetFirstDogPuppy];");
			migrationBuilder.Sql("DROP VIEW IF EXISTS dbo.GetPupiesWithParentDogName");
		}
	}
}
