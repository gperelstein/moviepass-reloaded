using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MPR.Auth.Data.Migrations
{
    public partial class AddIsActiveColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Profiles_UserId",
                schema: "mpr",
                table: "Profiles");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "mpr",
                table: "Profiles",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "mpr",
                table: "Profiles",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "mpr",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                schema: "mpr",
                table: "Profiles",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Profiles_UserId",
                schema: "mpr",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "mpr",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "mpr",
                table: "Profiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "mpr",
                table: "Profiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                schema: "mpr",
                table: "Profiles",
                column: "UserId");
        }
    }
}
