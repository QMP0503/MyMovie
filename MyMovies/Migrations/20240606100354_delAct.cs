using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMovies.Migrations
{
    /// <inheritdoc />
    public partial class delAct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieActors_Actors_ActorId",
                table: "MovieActors");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieDirectors_Directors_DirectorId",
                table: "MovieDirectors");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieActors_Actors_ActorId",
                table: "MovieActors",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieDirectors_Directors_DirectorId",
                table: "MovieDirectors",
                column: "DirectorId",
                principalTable: "Directors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieActors_Actors_ActorId",
                table: "MovieActors");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieDirectors_Directors_DirectorId",
                table: "MovieDirectors");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieActors_Actors_ActorId",
                table: "MovieActors",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieDirectors_Directors_DirectorId",
                table: "MovieDirectors",
                column: "DirectorId",
                principalTable: "Directors",
                principalColumn: "Id");
        }
    }
}
