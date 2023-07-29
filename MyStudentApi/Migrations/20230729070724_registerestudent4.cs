using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyStudentApi.Migrations
{
    /// <inheritdoc />
    public partial class registerestudent4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_SchoolClasses_SchoolClassID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SchoolClassID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IsRegistered",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SchoolClassID",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Students",
                newName: "FullName");

            migrationBuilder.AddColumn<int>(
                name: "CourseCode",
                table: "SchoolClasses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Course",
                table: "AttendanceViewModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRegistered",
                table: "AttendanceViewModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "AttendanceViewModel",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StopTime",
                table: "AttendanceViewModel",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StudentSchoolClass",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    SchoolClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSchoolClass", x => new { x.SchoolClassId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_StudentSchoolClass_SchoolClasses_SchoolClassId",
                        column: x => x.SchoolClassId,
                        principalTable: "SchoolClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSchoolClass_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentSchoolClass_StudentId",
                table: "StudentSchoolClass",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentSchoolClass");

            migrationBuilder.DropColumn(
                name: "CourseCode",
                table: "SchoolClasses");

            migrationBuilder.DropColumn(
                name: "Course",
                table: "AttendanceViewModel");

            migrationBuilder.DropColumn(
                name: "IsRegistered",
                table: "AttendanceViewModel");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "AttendanceViewModel");

            migrationBuilder.DropColumn(
                name: "StopTime",
                table: "AttendanceViewModel");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Students",
                newName: "Name");

            migrationBuilder.AddColumn<bool>(
                name: "IsRegistered",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SchoolClassID",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_SchoolClassID",
                table: "Students",
                column: "SchoolClassID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_SchoolClasses_SchoolClassID",
                table: "Students",
                column: "SchoolClassID",
                principalTable: "SchoolClasses",
                principalColumn: "Id");
        }
    }
}
