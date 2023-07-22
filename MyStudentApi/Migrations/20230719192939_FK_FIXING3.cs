using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyStudentApi.Migrations
{
    /// <inheritdoc />
    public partial class FK_FIXING3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddForeignKey(
                name: "FK_Students_SchoolClasses_SchoolClassId",
                table: "Students",
                column: "SchoolClassId",
                principalTable: "SchoolClasses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddForeignKey(
                name: "FK_Students_SchoolClasses_SchoolClassID",
                table: "Students",
                column: "SchoolClassID",
                principalTable: "SchoolClasses",
                principalColumn: "Id");
        }
    }
}
