using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cineVote.Migrations
{
    public partial class Results : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblCompetition_tblResult_ResultsResultId",
                table: "tblCompetition");

            migrationBuilder.DropForeignKey(
                name: "FK_tblResult_tblCategory_CategoryId",
                table: "tblResult");

            migrationBuilder.DropForeignKey(
                name: "FK_tblResult_tblSubscription_SubscriptionId",
                table: "tblResult");

            migrationBuilder.DropIndex(
                name: "IX_tblResult_CategoryId",
                table: "tblResult");

            migrationBuilder.DropIndex(
                name: "IX_tblResult_SubscriptionId",
                table: "tblResult");

            migrationBuilder.DropIndex(
                name: "IX_tblCompetition_ResultsResultId",
                table: "tblCompetition");

            migrationBuilder.DropColumn(
                name: "ResultsResultId",
                table: "tblCompetition");

            migrationBuilder.RenameColumn(
                name: "SubscriptionId",
                table: "tblResult",
                newName: "TotalParticipants");

            migrationBuilder.AddColumn<int>(
                name: "ResultId",
                table: "tblSubscription",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NomineeId",
                table: "tblResult",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Competition_Id",
                table: "tblResult",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Competition_Id1",
                table: "tblResult",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FirstPlaceId",
                table: "tblResult",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SecondPlaceId",
                table: "tblResult",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThirdPlaceId",
                table: "tblResult",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tblSubscription_ResultId",
                table: "tblSubscription",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_tblResult_Competition_Id1",
                table: "tblResult",
                column: "Competition_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_tblResult_FirstPlaceId",
                table: "tblResult",
                column: "FirstPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblResult_SecondPlaceId",
                table: "tblResult",
                column: "SecondPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblResult_ThirdPlaceId",
                table: "tblResult",
                column: "ThirdPlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblResult_tblCategoryNominee_FirstPlaceId",
                table: "tblResult",
                column: "FirstPlaceId",
                principalTable: "tblCategoryNominee",
                principalColumn: "CategoryNomineeKey",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblResult_tblCategoryNominee_SecondPlaceId",
                table: "tblResult",
                column: "SecondPlaceId",
                principalTable: "tblCategoryNominee",
                principalColumn: "CategoryNomineeKey",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblResult_tblCategoryNominee_ThirdPlaceId",
                table: "tblResult",
                column: "ThirdPlaceId",
                principalTable: "tblCategoryNominee",
                principalColumn: "CategoryNomineeKey",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblResult_tblCompetition_Competition_Id1",
                table: "tblResult",
                column: "Competition_Id1",
                principalTable: "tblCompetition",
                principalColumn: "Competition_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblSubscription_tblResult_ResultId",
                table: "tblSubscription",
                column: "ResultId",
                principalTable: "tblResult",
                principalColumn: "ResultID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblResult_tblCategoryNominee_FirstPlaceId",
                table: "tblResult");

            migrationBuilder.DropForeignKey(
                name: "FK_tblResult_tblCategoryNominee_SecondPlaceId",
                table: "tblResult");

            migrationBuilder.DropForeignKey(
                name: "FK_tblResult_tblCategoryNominee_ThirdPlaceId",
                table: "tblResult");

            migrationBuilder.DropForeignKey(
                name: "FK_tblResult_tblCompetition_Competition_Id1",
                table: "tblResult");

            migrationBuilder.DropForeignKey(
                name: "FK_tblSubscription_tblResult_ResultId",
                table: "tblSubscription");

            migrationBuilder.DropIndex(
                name: "IX_tblSubscription_ResultId",
                table: "tblSubscription");

            migrationBuilder.DropIndex(
                name: "IX_tblResult_Competition_Id1",
                table: "tblResult");

            migrationBuilder.DropIndex(
                name: "IX_tblResult_FirstPlaceId",
                table: "tblResult");

            migrationBuilder.DropIndex(
                name: "IX_tblResult_SecondPlaceId",
                table: "tblResult");

            migrationBuilder.DropIndex(
                name: "IX_tblResult_ThirdPlaceId",
                table: "tblResult");

            migrationBuilder.DropColumn(
                name: "ResultId",
                table: "tblSubscription");

            migrationBuilder.DropColumn(
                name: "Competition_Id",
                table: "tblResult");

            migrationBuilder.DropColumn(
                name: "Competition_Id1",
                table: "tblResult");

            migrationBuilder.DropColumn(
                name: "FirstPlaceId",
                table: "tblResult");

            migrationBuilder.DropColumn(
                name: "SecondPlaceId",
                table: "tblResult");

            migrationBuilder.DropColumn(
                name: "ThirdPlaceId",
                table: "tblResult");

            migrationBuilder.RenameColumn(
                name: "TotalParticipants",
                table: "tblResult",
                newName: "SubscriptionId");

            migrationBuilder.AlterColumn<int>(
                name: "NomineeId",
                table: "tblResult",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResultsResultId",
                table: "tblCompetition",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblResult_CategoryId",
                table: "tblResult",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblResult_SubscriptionId",
                table: "tblResult",
                column: "SubscriptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblCompetition_ResultsResultId",
                table: "tblCompetition",
                column: "ResultsResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblCompetition_tblResult_ResultsResultId",
                table: "tblCompetition",
                column: "ResultsResultId",
                principalTable: "tblResult",
                principalColumn: "ResultID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblResult_tblCategory_CategoryId",
                table: "tblResult",
                column: "CategoryId",
                principalTable: "tblCategory",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblResult_tblSubscription_SubscriptionId",
                table: "tblResult",
                column: "SubscriptionId",
                principalTable: "tblSubscription",
                principalColumn: "SubscriptionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
