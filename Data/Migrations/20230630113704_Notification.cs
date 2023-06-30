using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cineVote.Migrations
{
    public partial class Notification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "tblNotification",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionId",
                table: "tblNotification",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "userName",
                table: "tblNotification",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_tblNotification_SubscriptionId",
                table: "tblNotification",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblNotification_UserId",
                table: "tblNotification",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblNotification_tblSubscription_SubscriptionId",
                table: "tblNotification",
                column: "SubscriptionId",
                principalTable: "tblSubscription",
                principalColumn: "SubscriptionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblNotification_tblUser_UserId",
                table: "tblNotification",
                column: "UserId",
                principalTable: "tblUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblNotification_tblSubscription_SubscriptionId",
                table: "tblNotification");

            migrationBuilder.DropForeignKey(
                name: "FK_tblNotification_tblUser_UserId",
                table: "tblNotification");

            migrationBuilder.DropIndex(
                name: "IX_tblNotification_SubscriptionId",
                table: "tblNotification");

            migrationBuilder.DropIndex(
                name: "IX_tblNotification_UserId",
                table: "tblNotification");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "tblNotification");

            migrationBuilder.DropColumn(
                name: "userName",
                table: "tblNotification");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "tblNotification",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
