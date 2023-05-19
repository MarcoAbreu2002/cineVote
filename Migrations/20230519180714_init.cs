using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cineVote.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompetitionNominee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nomines",
                table: "Nomines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Competitions",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Nomines");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "imageURL",
                table: "Competitions");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "tblUsers");

            migrationBuilder.RenameTable(
                name: "Nomines",
                newName: "tblNominees");

            migrationBuilder.RenameTable(
                name: "Competitions",
                newName: "tblCompetition");

            migrationBuilder.RenameColumn(
                name: "userName",
                table: "tblUsers",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "tblUsers",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "tblUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "NomineeId",
                table: "tblNominees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Competition_Id",
                table: "tblCompetition",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Category",
                table: "tblCompetition",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "tblCompetition",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblUsers",
                table: "tblUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblNominees",
                table: "tblNominees",
                column: "NomineeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblCompetition",
                table: "tblCompetition",
                column: "Competition_Id");

            migrationBuilder.CreateTable(
                name: "tblCategory",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategory", x => x.CategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblCompetition_Category",
                table: "tblCompetition",
                column: "Category");

            migrationBuilder.AddForeignKey(
                name: "FK_tblCompetition_tblCategory_Category",
                table: "tblCompetition",
                column: "Category",
                principalTable: "tblCategory",
                principalColumn: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblCompetition_tblCategory_Category",
                table: "tblCompetition");

            migrationBuilder.DropTable(
                name: "tblCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblUsers",
                table: "tblUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblNominees",
                table: "tblNominees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblCompetition",
                table: "tblCompetition");

            migrationBuilder.DropIndex(
                name: "IX_tblCompetition_Category",
                table: "tblCompetition");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "tblUsers");

            migrationBuilder.DropColumn(
                name: "NomineeId",
                table: "tblNominees");

            migrationBuilder.DropColumn(
                name: "Competition_Id",
                table: "tblCompetition");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "tblCompetition");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "tblCompetition");

            migrationBuilder.RenameTable(
                name: "tblUsers",
                newName: "Persons");

            migrationBuilder.RenameTable(
                name: "tblNominees",
                newName: "Nomines");

            migrationBuilder.RenameTable(
                name: "tblCompetition",
                newName: "Competitions");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Persons",
                newName: "userName");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Persons",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Nomines",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Competitions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Competitions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "imageURL",
                table: "Competitions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nomines",
                table: "Nomines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Competitions",
                table: "Competitions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CompetitionNominee",
                columns: table => new
                {
                    CompetitionListId = table.Column<int>(type: "int", nullable: false),
                    NominesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionNominee", x => new { x.CompetitionListId, x.NominesId });
                    table.ForeignKey(
                        name: "FK_CompetitionNominee_Competitions_CompetitionListId",
                        column: x => x.CompetitionListId,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitionNominee_Nomines_NominesId",
                        column: x => x.NominesId,
                        principalTable: "Nomines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionNominee_NominesId",
                table: "CompetitionNominee",
                column: "NominesId");
        }
    }
}
