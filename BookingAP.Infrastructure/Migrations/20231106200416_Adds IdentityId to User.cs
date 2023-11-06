using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingAP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddsIdentityIdtoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityId",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_User_IdentityId",
                table: "User",
                column: "IdentityId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_IdentityId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "User");
        }
    }
}
