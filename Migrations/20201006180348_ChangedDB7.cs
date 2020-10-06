using Microsoft.EntityFrameworkCore.Migrations;

namespace BSOS.Migrations
{
    public partial class ChangedDB7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlProfile",
                table: "Comment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlProfile",
                table: "Comment");
        }
    }
}
