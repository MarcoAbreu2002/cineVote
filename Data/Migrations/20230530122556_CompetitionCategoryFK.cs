using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cineVote.Migrations
{
    public partial class CompetitionCategoryFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblCompetitionCategory_tblCompetition_CompetitionId",
                table: "tblCompetitionCategory");

            migrationBuilder.RenameColumn(
                name: "CompetitionId",
                table: "tblCompetitionCategory",
                newName: "Competition_Id1");

            migrationBuilder.RenameIndex(
                name: "IX_tblCompetitionCategory_CompetitionId",
                table: "tblCompetitionCategory",
                newName: "IX_tblCompetitionCategory_Competition_Id1");

            migrationBuilder.AddColumn<int>(
                name: "Competition_Id",
                table: "tblCompetitionCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_tblCompetitionCategory_tblCompetition_Competition_Id1",
                table: "tblCompetitionCategory",
                column: "Competition_Id1",
                principalTable: "tblCompetition",
                principalColumn: "Competition_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblCompetitionCategory_tblCompetition_Competition_Id1",
                table: "tblCompetitionCategory");

            migrationBuilder.DropColumn(
                name: "Competition_Id",
                table: "tblCompetitionCategory");

            migrationBuilder.RenameColumn(
                name: "Competition_Id1",
                table: "tblCompetitionCategory",
                newName: "CompetitionId");

            migrationBuilder.RenameIndex(
                name: "IX_tblCompetitionCategory_Competition_Id1",
                table: "tblCompetitionCategory",
                newName: "IX_tblCompetitionCategory_CompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblCompetitionCategory_tblCompetition_CompetitionId",
                table: "tblCompetitionCategory",
                column: "CompetitionId",
                principalTable: "tblCompetition",
                principalColumn: "Competition_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
