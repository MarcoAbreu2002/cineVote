using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cineVote.Migrations
{
    public partial class PostsWithUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "tblPosts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "userName",
                table: "tblPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "tblComments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "userName",
                table: "tblComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_tblPosts_UserId",
                table: "tblPosts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblComments_UserId",
                table: "tblComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblComments_tblUser_UserId",
                table: "tblComments",
                column: "UserId",
                principalTable: "tblUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblPosts_tblUser_UserId",
                table: "tblPosts",
                column: "UserId",
                principalTable: "tblUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblComments_tblUser_UserId",
                table: "tblComments");

            migrationBuilder.DropForeignKey(
                name: "FK_tblPosts_tblUser_UserId",
                table: "tblPosts");

            migrationBuilder.DropIndex(
                name: "IX_tblPosts_UserId",
                table: "tblPosts");

            migrationBuilder.DropIndex(
                name: "IX_tblComments_UserId",
                table: "tblComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "tblPosts");

            migrationBuilder.DropColumn(
                name: "userName",
                table: "tblPosts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "tblComments");

            migrationBuilder.DropColumn(
                name: "userName",
                table: "tblComments");
        }
    }
}
