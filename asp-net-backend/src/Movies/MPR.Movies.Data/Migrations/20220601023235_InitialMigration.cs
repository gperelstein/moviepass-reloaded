using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MPR.Movies.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mpr");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "Genres",
                schema: "mpr",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    TheMovieDbId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "varchar(255)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'"),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                schema: "mpr",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    TheMovieDbId = table.Column<int>(type: "integer", nullable: true),
                    Title = table.Column<string>(type: "varchar(255)", nullable: true),
                    Language = table.Column<string>(type: "varchar(255)", nullable: true),
                    Poster = table.Column<string>(type: "varchar(255)", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    TagLine = table.Column<string>(type: "varchar(255)", nullable: true),
                    Trailer = table.Column<string>(type: "varchar(255)", nullable: true),
                    MarkedAsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Owner = table.Column<Guid>(type: "uuid", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'"),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenres",
                schema: "mpr",
                columns: table => new
                {
                    GenresId = table.Column<Guid>(type: "uuid", nullable: false),
                    MoviesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => new { x.GenresId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_MovieGenres_Genres_GenresId",
                        column: x => x.GenresId,
                        principalSchema: "mpr",
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalSchema: "mpr",
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genres_TheMovieDbId",
                schema: "mpr",
                table: "Genres",
                column: "TheMovieDbId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_MoviesId",
                schema: "mpr",
                table: "MovieGenres",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_TheMovieDbId",
                schema: "mpr",
                table: "Movies",
                column: "TheMovieDbId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieGenres",
                schema: "mpr");

            migrationBuilder.DropTable(
                name: "Genres",
                schema: "mpr");

            migrationBuilder.DropTable(
                name: "Movies",
                schema: "mpr");
        }
    }
}
