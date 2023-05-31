using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cineVote.Migrations
{
    public partial class Vote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblVotes_tblUser_UserId1",
                table: "tblVotes");

            migrationBuilder.DropIndex(
                name: "IX_tblVotes_UserId1",
                table: "tblVotes");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "tblVotes");

            migrationBuilder.RenameColumn(
                name: "Vote_id",
                table: "tblVotes",
                newName: "VoteId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "tblVotes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "userName",
                table: "tblVotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "tblVoteSubscription",
                columns: table => new
                {
                    VoteSubscriptionKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    VoteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblVoteSubscription", x => x.VoteSubscriptionKey);
                    table.ForeignKey(
                        name: "FK_tblVoteSubscription_tblSubscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "tblSubscription",
                        principalColumn: "SubscriptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblVoteSubscription_tblVotes_VoteId",
                        column: x => x.VoteId,
                        principalTable: "tblVotes",
                        principalColumn: "VoteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblVotes_UserId",
                table: "tblVotes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblVoteSubscription_SubscriptionId",
                table: "tblVoteSubscription",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblVoteSubscription_VoteId",
                table: "tblVoteSubscription",
                column: "VoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblVotes_tblUser_UserId",
                table: "tblVotes",
                column: "UserId",
                principalTable: "tblUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblVotes_tblUser_UserId",
                table: "tblVotes");

            migrationBuilder.DropTable(
                name: "tblVoteSubscription");

            migrationBuilder.DropIndex(
                name: "IX_tblVotes_UserId",
                table: "tblVotes");

            migrationBuilder.DropColumn(
                name: "userName",
                table: "tblVotes");

            migrationBuilder.RenameColumn(
                name: "VoteId",
                table: "tblVotes",
                newName: "Vote_id");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "tblVotes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "tblVotes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_tblVotes_UserId1",
                table: "tblVotes",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_tblVotes_tblUser_UserId1",
                table: "tblVotes",
                column: "UserId1",
                principalTable: "tblUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
