using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cineVote.Migrations
{
    public partial class CompetitionsN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblNomineeCompetition_tblCompetition_CompetitionId",
                table: "tblNomineeCompetition");

            migrationBuilder.DropIndex(
                name: "IX_tblNomineeCompetition_CompetitionId",
                table: "tblNomineeCompetition");

            migrationBuilder.RenameColumn(
                name: "CompetitionId",
                table: "tblNomineeCompetition",
                newName: "Competition_Id");

            migrationBuilder.AddColumn<int>(
                name: "Competition_Id1",
                table: "tblNomineeCompetition",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblNomineeCompetition_Competition_Id1",
                table: "tblNomineeCompetition",
                column: "Competition_Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_tblNomineeCompetition_tblCompetition_Competition_Id1",
                table: "tblNomineeCompetition",
                column: "Competition_Id1",
                principalTable: "tblCompetition",
                principalColumn: "Competition_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblNomineeCompetition_tblCompetition_Competition_Id1",
                table: "tblNomineeCompetition");

            migrationBuilder.DropIndex(
                name: "IX_tblNomineeCompetition_Competition_Id1",
                table: "tblNomineeCompetition");

            migrationBuilder.DropColumn(
                name: "Competition_Id1",
                table: "tblNomineeCompetition");

            migrationBuilder.RenameColumn(
                name: "Competition_Id",
                table: "tblNomineeCompetition",
                newName: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblNomineeCompetition_CompetitionId",
                table: "tblNomineeCompetition",
                column: "CompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblNomineeCompetition_tblCompetition_CompetitionId",
                table: "tblNomineeCompetition",
                column: "CompetitionId",
                principalTable: "tblCompetition",
                principalColumn: "Competition_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
