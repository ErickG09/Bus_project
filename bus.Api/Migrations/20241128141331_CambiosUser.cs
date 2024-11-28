using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bus.Api.Migrations
{
    /// <inheritdoc />
    public partial class CambiosUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripDetails_AspNetUsers_UserId",
                table: "TripDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_TripDetails_AspNetUsers_UserId",
                table: "TripDetails",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripDetails_AspNetUsers_UserId",
                table: "TripDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_TripDetails_AspNetUsers_UserId",
                table: "TripDetails",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
