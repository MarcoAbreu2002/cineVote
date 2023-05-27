using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cineVote.Migrations
{
    public partial class UpdateCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblCategory_tblCompetition_CompetitionId",
                table: "tblCategory");

            migrationBuilder.DropIndex(
                name: "IX_tblResult_CategoryId",
                table: "tblResult");

            migrationBuilder.DropIndex(
                name: "IX_tblCategory_CompetitionId",
                table: "tblCategory");

            migrationBuilder.DropColumn(
                name: "CompetitionId",
                table: "tblCategory");

            migrationBuilder.AddColumn<int>(
                name: "Competition_Id",
                table: "tblCategory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblResult_CategoryId",
                table: "tblResult",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCategory_Competition_Id",
                table: "tblCategory",
                column: "Competition_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblCategory_tblCompetition_Competition_Id",
                table: "tblCategory",
                column: "Competition_Id",
                principalTable: "tblCompetition",
                principalColumn: "Competition_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblCategory_tblCompetition_Competition_Id",
                table: "tblCategory");

            migrationBuilder.DropIndex(
                name: "IX_tblResult_CategoryId",
                table: "tblResult");

            migrationBuilder.DropIndex(
                name: "IX_tblCategory_Competition_Id",
                table: "tblCategory");

            migrationBuilder.DropColumn(
                name: "Competition_Id",
                table: "tblCategory");

            migrationBuilder.AddColumn<int>(
                name: "CompetitionId",
                table: "tblCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tblResult_CategoryId",
                table: "tblResult",
                column: "CategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblCategory_CompetitionId",
                table: "tblCategory",
                column: "CompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblCategory_tblCompetition_CompetitionId",
                table: "tblCategory",
                column: "CompetitionId",
                principalTable: "tblCompetition",
                principalColumn: "Competition_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
