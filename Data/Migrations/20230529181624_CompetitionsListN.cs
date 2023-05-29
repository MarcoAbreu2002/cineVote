using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cineVote.Migrations
{
    public partial class CompetitionsListN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblSubscription_tblCompetition_Competition_Id",
                table: "tblSubscription");

            migrationBuilder.DropIndex(
                name: "IX_tblSubscription_Competition_Id",
                table: "tblSubscription");

            migrationBuilder.RenameColumn(
                name: "Subscription_id",
                table: "tblSubscription",
                newName: "SubscriptionId");

            migrationBuilder.AddColumn<int>(
                name: "Competition_Id1",
                table: "tblSubscription",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblSubscription_Competition_Id1",
                table: "tblSubscription",
                column: "Competition_Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_tblSubscription_tblCompetition_Competition_Id1",
                table: "tblSubscription",
                column: "Competition_Id1",
                principalTable: "tblCompetition",
                principalColumn: "Competition_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblSubscription_tblCompetition_Competition_Id1",
                table: "tblSubscription");

            migrationBuilder.DropIndex(
                name: "IX_tblSubscription_Competition_Id1",
                table: "tblSubscription");

            migrationBuilder.DropColumn(
                name: "Competition_Id1",
                table: "tblSubscription");

            migrationBuilder.RenameColumn(
                name: "SubscriptionId",
                table: "tblSubscription",
                newName: "Subscription_id");

            migrationBuilder.CreateIndex(
                name: "IX_tblSubscription_Competition_Id",
                table: "tblSubscription",
                column: "Competition_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblSubscription_tblCompetition_Competition_Id",
                table: "tblSubscription",
                column: "Competition_Id",
                principalTable: "tblCompetition",
                principalColumn: "Competition_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
