using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cineVote.Migrations
{
    public partial class CompetitionsID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Competition_Id",
                table: "tblNominee",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblNominee_Competition_Id",
                table: "tblNominee",
                column: "Competition_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblNominee_tblCompetition_Competition_Id",
                table: "tblNominee",
                column: "Competition_Id",
                principalTable: "tblCompetition",
                principalColumn: "Competition_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblNominee_tblCompetition_Competition_Id",
                table: "tblNominee");

            migrationBuilder.DropIndex(
                name: "IX_tblNominee_Competition_Id",
                table: "tblNominee");

            migrationBuilder.DropColumn(
                name: "Competition_Id",
                table: "tblNominee");
        }
    }
}
