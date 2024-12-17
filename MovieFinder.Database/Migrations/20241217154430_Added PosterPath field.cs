using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieFinder.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedPosterPathfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PosterPath",
                table: "WatchedMovies",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosterPath",
                table: "WatchedMovies");
        }
    }
}
