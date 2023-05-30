using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cineVote.Migrations
{
    public partial class Subscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblVotes_tblSubscription_SubscriptionId",
                table: "tblVotes");

            migrationBuilder.DropTable(
                name: "SubscriptionUser");

            migrationBuilder.DropIndex(
                name: "IX_tblVotes_SubscriptionId",
                table: "tblVotes");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionId1",
                table: "tblVotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "tblSubscription",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VoteId",
                table: "tblSubscription",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userName",
                table: "tblSubscription",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_tblVotes_SubscriptionId1",
                table: "tblVotes",
                column: "SubscriptionId1");

            migrationBuilder.CreateIndex(
                name: "IX_tblSubscription_UserId",
                table: "tblSubscription",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblSubscription_tblUser_UserId",
                table: "tblSubscription",
                column: "UserId",
                principalTable: "tblUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblVotes_tblSubscription_SubscriptionId1",
                table: "tblVotes",
                column: "SubscriptionId1",
                principalTable: "tblSubscription",
                principalColumn: "SubscriptionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblSubscription_tblUser_UserId",
                table: "tblSubscription");

            migrationBuilder.DropForeignKey(
                name: "FK_tblVotes_tblSubscription_SubscriptionId1",
                table: "tblVotes");

            migrationBuilder.DropIndex(
                name: "IX_tblVotes_SubscriptionId1",
                table: "tblVotes");

            migrationBuilder.DropIndex(
                name: "IX_tblSubscription_UserId",
                table: "tblSubscription");

            migrationBuilder.DropColumn(
                name: "SubscriptionId1",
                table: "tblVotes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "tblSubscription");

            migrationBuilder.DropColumn(
                name: "VoteId",
                table: "tblSubscription");

            migrationBuilder.DropColumn(
                name: "userName",
                table: "tblSubscription");

            migrationBuilder.CreateTable(
                name: "SubscriptionUser",
                columns: table => new
                {
                    UsersRegisteredId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    subscritionsSubscriptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionUser", x => new { x.UsersRegisteredId, x.subscritionsSubscriptionId });
                    table.ForeignKey(
                        name: "FK_SubscriptionUser_tblSubscription_subscritionsSubscriptionId",
                        column: x => x.subscritionsSubscriptionId,
                        principalTable: "tblSubscription",
                        principalColumn: "SubscriptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionUser_tblUser_UsersRegisteredId",
                        column: x => x.UsersRegisteredId,
                        principalTable: "tblUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblVotes_SubscriptionId",
                table: "tblVotes",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionUser_subscritionsSubscriptionId",
                table: "SubscriptionUser",
                column: "subscritionsSubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblVotes_tblSubscription_SubscriptionId",
                table: "tblVotes",
                column: "SubscriptionId",
                principalTable: "tblSubscription",
                principalColumn: "SubscriptionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
