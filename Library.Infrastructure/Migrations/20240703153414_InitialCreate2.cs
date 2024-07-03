using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LibrarianId",
                table: "Rentals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_LibrarianId",
                table: "Rentals",
                column: "LibrarianId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Librarians_LibrarianId",
                table: "Rentals",
                column: "LibrarianId",
                principalTable: "Librarians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Librarians_LibrarianId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_LibrarianId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "LibrarianId",
                table: "Rentals");
        }
    }
}
