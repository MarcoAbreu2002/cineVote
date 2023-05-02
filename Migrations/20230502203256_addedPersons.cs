using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cineVote.Migrations
{
    /// <inheritdoc />
    public partial class addedPersons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionNominee_Nominee_NominesId",
                table: "CompetitionNominee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nominee",
                table: "Nominee");

            migrationBuilder.RenameTable(
                name: "Nominee",
                newName: "Nomines");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nomines",
                table: "Nomines",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adminId = table.Column<int>(type: "int", nullable: true),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CompetitionNominee_Nomines_NominesId",
                table: "CompetitionNominee",
                column: "NominesId",
                principalTable: "Nomines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionNominee_Nomines_NominesId",
                table: "CompetitionNominee");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nomines",
                table: "Nomines");

            migrationBuilder.RenameTable(
                name: "Nomines",
                newName: "Nominee");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nominee",
                table: "Nominee",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetitionNominee_Nominee_NominesId",
                table: "CompetitionNominee",
                column: "NominesId",
                principalTable: "Nominee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
