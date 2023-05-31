using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cineVote.Migrations
{
    public partial class ResultsChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_tblResult_tblNominee_NomineeId",
                table: "tblResult");

            migrationBuilder.DropTable(
                name: "ParticipantRole");

            migrationBuilder.DropTable(
                name: "tblParticipant");

            migrationBuilder.DropTable(
                name: "tblRole");

            migrationBuilder.DropTable(
                name: "tblMovie");

            migrationBuilder.DropIndex(
                name: "IX_tblResult_NomineeId",
                table: "tblResult");

            migrationBuilder.DropColumn(
                name: "NomineeId",
                table: "tblResult");

            migrationBuilder.AddForeignKey(
                name: "FK_tblResult_tblNominee_FirstPlaceId",
                table: "tblResult",
                column: "FirstPlaceId",
                principalTable: "tblNominee",
                principalColumn: "NomineeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblResult_tblNominee_SecondPlaceId",
                table: "tblResult",
                column: "SecondPlaceId",
                principalTable: "tblNominee",
                principalColumn: "NomineeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblResult_tblNominee_ThirdPlaceId",
                table: "tblResult",
                column: "ThirdPlaceId",
                principalTable: "tblNominee",
                principalColumn: "NomineeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblResult_tblNominee_FirstPlaceId",
                table: "tblResult");

            migrationBuilder.DropForeignKey(
                name: "FK_tblResult_tblNominee_SecondPlaceId",
                table: "tblResult");

            migrationBuilder.DropForeignKey(
                name: "FK_tblResult_tblNominee_ThirdPlaceId",
                table: "tblResult");

            migrationBuilder.AddColumn<int>(
                name: "NomineeId",
                table: "tblResult",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblMovie",
                columns: table => new
                {
                    NomineeId = table.Column<int>(type: "int", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMovie", x => x.NomineeId);
                    table.ForeignKey(
                        name: "FK_tblMovie_tblNominee_NomineeId",
                        column: x => x.NomineeId,
                        principalTable: "tblNominee",
                        principalColumn: "NomineeId");
                });

            migrationBuilder.CreateTable(
                name: "tblParticipant",
                columns: table => new
                {
                    NomineeId = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblParticipant", x => x.NomineeId);
                    table.ForeignKey(
                        name: "FK_tblParticipant_tblNominee_NomineeId",
                        column: x => x.NomineeId,
                        principalTable: "tblNominee",
                        principalColumn: "NomineeId");
                });

            migrationBuilder.CreateTable(
                name: "tblRole",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRole", x => x.RoleID);
                    table.ForeignKey(
                        name: "FK_tblRole_tblMovie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "tblMovie",
                        principalColumn: "NomineeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantRole",
                columns: table => new
                {
                    ParticipantsNomineeId = table.Column<int>(type: "int", nullable: false),
                    RolesRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantRole", x => new { x.ParticipantsNomineeId, x.RolesRoleId });
                    table.ForeignKey(
                        name: "FK_ParticipantRole_tblParticipant_ParticipantsNomineeId",
                        column: x => x.ParticipantsNomineeId,
                        principalTable: "tblParticipant",
                        principalColumn: "NomineeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParticipantRole_tblRole_RolesRoleId",
                        column: x => x.RolesRoleId,
                        principalTable: "tblRole",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblResult_NomineeId",
                table: "tblResult",
                column: "NomineeId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantRole_RolesRoleId",
                table: "ParticipantRole",
                column: "RolesRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRole_MovieId",
                table: "tblRole",
                column: "MovieId");

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
                name: "FK_tblResult_tblNominee_NomineeId",
                table: "tblResult",
                column: "NomineeId",
                principalTable: "tblNominee",
                principalColumn: "NomineeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
