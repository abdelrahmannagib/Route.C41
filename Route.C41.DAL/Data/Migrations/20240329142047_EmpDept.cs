using Microsoft.EntityFrameworkCore.Migrations;

namespace Route.C41.DAL.Data.Migrations
{
    public partial class EmpDept : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Departments",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Employees",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentID",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_Id",
                table: "Employees",
                column: "Id",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_Id",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DepartmentID",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Departments",
                newName: "ID");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Employees",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
