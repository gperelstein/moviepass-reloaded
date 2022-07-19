using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MPR.Movies.Data.Migrations
{
    public partial class AddRemovableEntityFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "mpr",
                table: "Movies",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "mpr",
                table: "Movies",
                type: "uuid",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "mpr",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "mpr",
                table: "Movies");
        }
    }
}
