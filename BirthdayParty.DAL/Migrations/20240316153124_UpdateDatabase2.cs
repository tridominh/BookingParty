using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirthdayParty.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingService_Bookings_BookingId",
                table: "BookingService");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingService_Services_ServiceId",
                table: "BookingService");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingService",
                table: "BookingService");

            migrationBuilder.RenameTable(
                name: "BookingService",
                newName: "BookingServices");

            migrationBuilder.RenameIndex(
                name: "IX_BookingService_ServiceId",
                table: "BookingServices",
                newName: "IX_BookingServices_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingService_BookingId",
                table: "BookingServices",
                newName: "IX_BookingServices_BookingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingServices",
                table: "BookingServices",
                column: "BookingServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingServices_Bookings_BookingId",
                table: "BookingServices",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingServices_Services_ServiceId",
                table: "BookingServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingServices_Bookings_BookingId",
                table: "BookingServices");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingServices_Services_ServiceId",
                table: "BookingServices");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingServices",
                table: "BookingServices");

            migrationBuilder.RenameTable(
                name: "BookingServices",
                newName: "BookingService");

            migrationBuilder.RenameIndex(
                name: "IX_BookingServices_ServiceId",
                table: "BookingService",
                newName: "IX_BookingService_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingServices_BookingId",
                table: "BookingService",
                newName: "IX_BookingService_BookingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingService",
                table: "BookingService",
                column: "BookingServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingService_Bookings_BookingId",
                table: "BookingService",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingService_Services_ServiceId",
                table: "BookingService",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
