using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyStudentApi.Migrations
{
    /// <inheritdoc />
    public partial class fk_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_SchoolClasses_SchoolClassId",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Students_StudentId",
                table: "Attendance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance");

            migrationBuilder.RenameTable(
                name: "Attendance",
                newName: "AttendanceViewModel");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "SchoolClasses",
                newName: "StartTime");

            migrationBuilder.RenameIndex(
                name: "IX_Attendance_StudentId",
                table: "AttendanceViewModel",
                newName: "IX_AttendanceViewModel_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendance_SchoolClassId",
                table: "AttendanceViewModel",
                newName: "IX_AttendanceViewModel_SchoolClassId");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StopTime",
                table: "SchoolClasses",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttendanceViewModel",
                table: "AttendanceViewModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceViewModel_SchoolClasses_SchoolClassId",
                table: "AttendanceViewModel",
                column: "SchoolClassId",
                principalTable: "SchoolClasses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceViewModel_Students_StudentId",
                table: "AttendanceViewModel",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceViewModel_SchoolClasses_SchoolClassId",
                table: "AttendanceViewModel");

            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceViewModel_Students_StudentId",
                table: "AttendanceViewModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttendanceViewModel",
                table: "AttendanceViewModel");

            migrationBuilder.DropColumn(
                name: "StopTime",
                table: "SchoolClasses");

            migrationBuilder.RenameTable(
                name: "AttendanceViewModel",
                newName: "Attendance");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "SchoolClasses",
                newName: "Duration");

            migrationBuilder.RenameIndex(
                name: "IX_AttendanceViewModel_StudentId",
                table: "Attendance",
                newName: "IX_Attendance_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_AttendanceViewModel_SchoolClassId",
                table: "Attendance",
                newName: "IX_Attendance_SchoolClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_SchoolClasses_SchoolClassId",
                table: "Attendance",
                column: "SchoolClassId",
                principalTable: "SchoolClasses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Students_StudentId",
                table: "Attendance",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
