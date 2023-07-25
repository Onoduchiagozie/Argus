using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyStudentApi.Migrations
{
    /// <inheritdoc />
    public partial class SubmitFUNC3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceViewModel_SchoolClasses_SchoolClassId",
                table: "AttendanceViewModel");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceViewModel_SchoolClasses_SchoolClassId",
                table: "AttendanceViewModel");

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
        }
    }
}
