using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cineVote.Migrations
{
    public partial class AddUserImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "newImageUrl",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "newImageUrl",
                table: "AspNetUsers");
        }
    }
}
