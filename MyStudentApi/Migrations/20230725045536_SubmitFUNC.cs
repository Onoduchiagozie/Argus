using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyStudentApi.Migrations
{
    /// <inheritdoc />
    public partial class SubmitFUNC : Migration
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

            migrationBuilder.AddColumn<bool>(
                name: "IsRegistered",
                table: "AttendanceViewModel",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRegistered",
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
