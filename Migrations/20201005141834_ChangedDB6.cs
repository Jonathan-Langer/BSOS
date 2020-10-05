using Microsoft.EntityFrameworkCore.Migrations;

namespace BSOS.Migrations
{
    public partial class ChangedDB6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Urlimage",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Urlimage",
                table: "Products");
        }
    }
}
