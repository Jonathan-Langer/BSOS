﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace BSOS.Migrations
{
    public partial class ChangedDB5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Customers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Customers");
        }
    }
}
