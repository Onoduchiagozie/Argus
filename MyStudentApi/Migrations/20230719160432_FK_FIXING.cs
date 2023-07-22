using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyStudentApi.Migrations
{
    /// <inheritdoc />
    public partial class FK_FIXING : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceViewModel_SchoolClasses_SchoolClassId",
                table: "AttendanceViewModel");

            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceViewModel_Students_StudentId",
                table: "AttendanceViewModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_SchoolClasses_SchoolClassId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "SchoolClassId",
                table: "Students",
                newName: "SchoolClassID");

            migrationBuilder.RenameIndex(
                name: "IX_Students_SchoolClassId",
                table: "Students",
                newName: "IX_Students_SchoolClassID");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "AttendanceViewModel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SchoolClassId",
                table: "AttendanceViewModel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceViewModel_SchoolClasses_SchoolClassId",
                table: "AttendanceViewModel",
                column: "SchoolClassId",
                principalTable: "SchoolClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceViewModel_Students_StudentId",
                table: "AttendanceViewModel",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_SchoolClasses_SchoolClassID",
                table: "Students",
                column: "SchoolClassID",
                principalTable: "SchoolClasses",
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

            migrationBuilder.DropForeignKey(
                name: "FK_Students_SchoolClasses_SchoolClassID",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "SchoolClassID",
                table: "Students",
                newName: "SchoolClassId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_SchoolClassID",
                table: "Students",
                newName: "IX_Students_SchoolClassId");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "AttendanceViewModel",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SchoolClassId",
                table: "AttendanceViewModel",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Students_SchoolClasses_SchoolClassId",
                table: "Students",
                column: "SchoolClassId",
                principalTable: "SchoolClasses",
                principalColumn: "Id");
        }
    }
}
