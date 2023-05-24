using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cineVote.Migrations
{
    public partial class UpdateAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblCompetition_tblAdmin_AdminId1",
                table: "tblCompetition");

            migrationBuilder.DropIndex(
                name: "IX_tblCompetition_AdminId1",
                table: "tblCompetition");

            migrationBuilder.DropColumn(
                name: "AdminId1",
                table: "tblCompetition");

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "tblCompetition",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_tblCompetition_AdminId",
                table: "tblCompetition",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblCompetition_tblAdmin_AdminId",
                table: "tblCompetition",
                column: "AdminId",
                principalTable: "tblAdmin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblCompetition_tblAdmin_AdminId",
                table: "tblCompetition");

            migrationBuilder.DropIndex(
                name: "IX_tblCompetition_AdminId",
                table: "tblCompetition");

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                table: "tblCompetition",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AdminId1",
                table: "tblCompetition",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_tblCompetition_AdminId1",
                table: "tblCompetition",
                column: "AdminId1");

            migrationBuilder.AddForeignKey(
                name: "FK_tblCompetition_tblAdmin_AdminId1",
                table: "tblCompetition",
                column: "AdminId1",
                principalTable: "tblAdmin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
