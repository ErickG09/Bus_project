﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bus.Api.Migrations
{
    /// <inheritdoc />
    public partial class Autenticacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripDetails_AspNetUsers_UserId",
                table: "TripDetails");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TripDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TripDetails_AspNetUsers_UserId",
                table: "TripDetails",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripDetails_AspNetUsers_UserId",
                table: "TripDetails");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TripDetails",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_TripDetails_AspNetUsers_UserId",
                table: "TripDetails",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
