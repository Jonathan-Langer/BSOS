using Microsoft.EntityFrameworkCore.Migrations;

namespace BSOS.Migrations
{
    public partial class ChangedDB10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "Roles",
                table: "Customers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Roles",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
