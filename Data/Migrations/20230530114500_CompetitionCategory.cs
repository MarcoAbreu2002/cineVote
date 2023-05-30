using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cineVote.Migrations
{
    public partial class CompetitionCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCompetitionCategory",
                columns: table => new
                {
                    CompetitionCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitionId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCompetitionCategory", x => x.CompetitionCategoryId);
                    table.ForeignKey(
                        name: "FK_tblCompetitionCategory_tblCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tblCategory",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblCompetitionCategory_tblCompetition_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "tblCompetition",
                        principalColumn: "Competition_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblCompetitionCategory_CategoryId",
                table: "tblCompetitionCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCompetitionCategory_CompetitionId",
                table: "tblCompetitionCategory",
                column: "CompetitionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblCompetitionCategory");
        }
    }
}
