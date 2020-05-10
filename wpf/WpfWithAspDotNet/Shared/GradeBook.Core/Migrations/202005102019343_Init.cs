namespace GradeBook.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SchoolSubjectId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        GradeValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SchoolSubjects", t => t.SchoolSubjectId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.SchoolSubjectId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.SchoolSubjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Lastname = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.Name, t.Lastname }, unique: true, name: "Unq_Student_Name_Lastname");
            
            CreateTable(
                "dbo.SchoolSubjectStudents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        SchoolSubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SchoolSubjects", t => t.SchoolSubjectId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.SchoolSubjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchoolSubjectStudents", "StudentId", "dbo.Students");
            DropForeignKey("dbo.SchoolSubjectStudents", "SchoolSubjectId", "dbo.SchoolSubjects");
            DropForeignKey("dbo.Grades", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Grades", "SchoolSubjectId", "dbo.SchoolSubjects");
            DropIndex("dbo.SchoolSubjectStudents", new[] { "SchoolSubjectId" });
            DropIndex("dbo.SchoolSubjectStudents", new[] { "StudentId" });
            DropIndex("dbo.Students", "Unq_Student_Name_Lastname");
            DropIndex("dbo.Grades", new[] { "StudentId" });
            DropIndex("dbo.Grades", new[] { "SchoolSubjectId" });
            DropTable("dbo.SchoolSubjectStudents");
            DropTable("dbo.Students");
            DropTable("dbo.SchoolSubjects");
            DropTable("dbo.Grades");
        }
    }
}
