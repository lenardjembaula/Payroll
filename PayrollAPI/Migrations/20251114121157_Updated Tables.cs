using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payslip_Employees_EmployeeId",
                table: "Payslip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payslip",
                table: "Payslip");

            migrationBuilder.RenameTable(
                name: "Payslip",
                newName: "Payslips");

            migrationBuilder.RenameIndex(
                name: "IX_Payslip_EmployeeId",
                table: "Payslips",
                newName: "IX_Payslips_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payslips",
                table: "Payslips",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payslips_Employees_EmployeeId",
                table: "Payslips",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payslips_Employees_EmployeeId",
                table: "Payslips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payslips",
                table: "Payslips");

            migrationBuilder.RenameTable(
                name: "Payslips",
                newName: "Payslip");

            migrationBuilder.RenameIndex(
                name: "IX_Payslips_EmployeeId",
                table: "Payslip",
                newName: "IX_Payslip_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payslip",
                table: "Payslip",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payslip_Employees_EmployeeId",
                table: "Payslip",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
