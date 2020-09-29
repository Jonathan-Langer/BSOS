using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BSOS.Migrations
{
    public partial class ChangedDB3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Products_CustomerID",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Customers_ProductID",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_CustomerID",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Comment",
                newName: "ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Comment",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Comment",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Comment",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IP",
                table: "Comment",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Posted",
                table: "Comment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SentBy",
                table: "Comment",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Comment",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ProductId",
                table: "Comment",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Products_ProductId",
                table: "Comment",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Products_ProductId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_ProductId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Body",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "IP",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Posted",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "SentBy",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Comment",
                newName: "ProductID");

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "Comment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                columns: new[] { "ProductID", "CustomerID" });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_CustomerID",
                table: "Comment",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Products_CustomerID",
                table: "Comment",
                column: "CustomerID",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Customers_ProductID",
                table: "Comment",
                column: "ProductID",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
