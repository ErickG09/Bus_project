using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bus.Api.Migrations
{
    /// <inheritdoc />
    public partial class Relaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buses_Companies_CompanyId",
                table: "Buses");

            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_Trips_TripId",
                table: "Passengers");

            migrationBuilder.DropForeignKey(
                name: "FK_TripDetails_Passengers_PassengerId",
                table: "TripDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Buses_BusId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Passengers_TripId",
                table: "Passengers");

            migrationBuilder.DropIndex(
                name: "IX_Buses_Company_Category",
                table: "Buses");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "Buses");

            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "TripDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OriginId",
                table: "TripDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Buses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DriverId",
                table: "Trips",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_TripDetails_DestinationId",
                table: "TripDetails",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_TripDetails_OriginId",
                table: "TripDetails",
                column: "OriginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_Companies_CompanyId",
                table: "Buses",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripDetails_Destinations_DestinationId",
                table: "TripDetails",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripDetails_Origins_OriginId",
                table: "TripDetails",
                column: "OriginId",
                principalTable: "Origins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripDetails_Passengers_PassengerId",
                table: "TripDetails",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Buses_BusId",
                table: "Trips",
                column: "BusId",
                principalTable: "Buses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Drivers_DriverId",
                table: "Trips",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buses_Companies_CompanyId",
                table: "Buses");

            migrationBuilder.DropForeignKey(
                name: "FK_TripDetails_Destinations_DestinationId",
                table: "TripDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TripDetails_Origins_OriginId",
                table: "TripDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TripDetails_Passengers_PassengerId",
                table: "TripDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Buses_BusId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Drivers_DriverId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_DriverId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_TripDetails_DestinationId",
                table: "TripDetails");

            migrationBuilder.DropIndex(
                name: "IX_TripDetails_OriginId",
                table: "TripDetails");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "TripDetails");

            migrationBuilder.DropColumn(
                name: "OriginId",
                table: "TripDetails");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Trips",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TripId",
                table: "Passengers",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Buses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Buses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_TripId",
                table: "Passengers",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Buses_Company_Category",
                table: "Buses",
                columns: new[] { "Company", "Category" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_Companies_CompanyId",
                table: "Buses",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_Trips_TripId",
                table: "Passengers",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TripDetails_Passengers_PassengerId",
                table: "TripDetails",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Buses_BusId",
                table: "Trips",
                column: "BusId",
                principalTable: "Buses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
