using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LessonService.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "lessonsrv");

            migrationBuilder.CreateTable(
                name: "Lessons",
                schema: "lessonsrv",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DateFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false, defaultValue: 60),
                    TrainingLevel = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    LessonType = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    MaxStudents = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    LessonStatus = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    TrainerId = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LessonGroups",
                schema: "lessonsrv",
                columns: table => new
                {
                    LessonId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonGroups", x => new { x.StudentId, x.LessonId });
                    table.ForeignKey(
                        name: "FK_LessonGroups_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalSchema: "lessonsrv",
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "lessonsrv",
                table: "Lessons",
                columns: new[] { "Id", "DateFrom", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("2f7449c3-c1a4-415f-a5f4-bd02ced6884a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description 2", "Lesson 2" },
                    { new Guid("acfd9260-55c4-44a2-a18a-e1953ba9f2cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description 1", "Lesson 1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessonGroups_LessonId",
                schema: "lessonsrv",
                table: "LessonGroups",
                column: "LessonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonGroups",
                schema: "lessonsrv");

            migrationBuilder.DropTable(
                name: "Lessons",
                schema: "lessonsrv");
        }
    }
}
