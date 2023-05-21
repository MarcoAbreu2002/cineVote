using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cineVote.Migrations
{
    public partial class fDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblNominee",
                columns: table => new
                {
                    NomineeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfilePictureURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblNominee", x => x.NomineeId);
                });

            migrationBuilder.CreateTable(
                name: "tblNotification",
                columns: table => new
                {
                    notificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblNotification", x => x.notificationId);
                });

            migrationBuilder.CreateTable(
                name: "tblPerson",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    imageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPerson", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "tblMovie",
                columns: table => new
                {
                    NomineeId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true)
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
                name: "tblAdmin",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Competition_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAdmin", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_tblAdmin_tblPerson_PersonId",
                        column: x => x.PersonId,
                        principalTable: "tblPerson",
                        principalColumn: "PersonId");
                });

            migrationBuilder.CreateTable(
                name: "tblUser",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUser", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_tblUser_tblPerson_PersonId",
                        column: x => x.PersonId,
                        principalTable: "tblPerson",
                        principalColumn: "PersonId");
                });

            migrationBuilder.CreateTable(
                name: "tblRole",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "SubscriptionUser",
                columns: table => new
                {
                    UsersRegisteredPersonId = table.Column<int>(type: "int", nullable: false),
                    subscritionsSubscriptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionUser", x => new { x.UsersRegisteredPersonId, x.subscritionsSubscriptionId });
                    table.ForeignKey(
                        name: "FK_SubscriptionUser_tblUser_UsersRegisteredPersonId",
                        column: x => x.UsersRegisteredPersonId,
                        principalTable: "tblUser",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompetitionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategory", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "tblCategoryNominee",
                columns: table => new
                {
                    CategoryNomineeKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    NomineeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategoryNominee", x => x.CategoryNomineeKey);
                    table.ForeignKey(
                        name: "FK_tblCategoryNominee_tblCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tblCategory",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblCategoryNominee_tblNominee_NomineeId",
                        column: x => x.NomineeId,
                        principalTable: "tblNominee",
                        principalColumn: "NomineeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblCompetition",
                columns: table => new
                {
                    Competition_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    ResultsResultId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCompetition", x => x.Competition_Id);
                    table.ForeignKey(
                        name: "FK_tblCompetition_tblAdmin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "tblAdmin",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblNomineeCompetition",
                columns: table => new
                {
                    NomineeCompetitionKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomineeId = table.Column<int>(type: "int", nullable: false),
                    CompetitionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblNomineeCompetition", x => x.NomineeCompetitionKey);
                    table.ForeignKey(
                        name: "FK_tblNomineeCompetition_tblCompetition_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "tblCompetition",
                        principalColumn: "Competition_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblNomineeCompetition_tblNominee_NomineeId",
                        column: x => x.NomineeId,
                        principalTable: "tblNominee",
                        principalColumn: "NomineeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSubscription",
                columns: table => new
                {
                    Subscription_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subscription_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Competition_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSubscription", x => x.Subscription_id);
                    table.ForeignKey(
                        name: "FK_tblSubscription_tblCompetition_Competition_Id",
                        column: x => x.Competition_Id,
                        principalTable: "tblCompetition",
                        principalColumn: "Competition_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblResult",
                columns: table => new
                {
                    ResultID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomineeId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblResult", x => x.ResultID);
                    table.ForeignKey(
                        name: "FK_tblResult_tblCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tblCategory",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblResult_tblNominee_NomineeId",
                        column: x => x.NomineeId,
                        principalTable: "tblNominee",
                        principalColumn: "NomineeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblResult_tblSubscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "tblSubscription",
                        principalColumn: "Subscription_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSubscriptionNotifications",
                columns: table => new
                {
                    SubscriptionNotificationsKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSubscriptionNotifications", x => x.SubscriptionNotificationsKey);
                    table.ForeignKey(
                        name: "FK_tblSubscriptionNotifications_tblNotification_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "tblNotification",
                        principalColumn: "notificationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSubscriptionNotifications_tblSubscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "tblSubscription",
                        principalColumn: "Subscription_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblVotes",
                columns: table => new
                {
                    Vote_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    NomineeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblVotes", x => x.Vote_id);
                    table.ForeignKey(
                        name: "FK_tblVotes_tblCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tblCategory",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblVotes_tblNominee_NomineeId",
                        column: x => x.NomineeId,
                        principalTable: "tblNominee",
                        principalColumn: "NomineeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblVotes_tblSubscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "tblSubscription",
                        principalColumn: "Subscription_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblVotes_tblUser_UserId",
                        column: x => x.UserId,
                        principalTable: "tblUser",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantRole_RolesRoleId",
                table: "ParticipantRole",
                column: "RolesRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionUser_subscritionsSubscriptionId",
                table: "SubscriptionUser",
                column: "subscritionsSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCategory_CompetitionId",
                table: "tblCategory",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCategoryNominee_CategoryId",
                table: "tblCategoryNominee",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCategoryNominee_NomineeId",
                table: "tblCategoryNominee",
                column: "NomineeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCompetition_AdminId",
                table: "tblCompetition",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCompetition_ResultsResultId",
                table: "tblCompetition",
                column: "ResultsResultId");

            migrationBuilder.CreateIndex(
                name: "IX_tblNomineeCompetition_CompetitionId",
                table: "tblNomineeCompetition",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblNomineeCompetition_NomineeId",
                table: "tblNomineeCompetition",
                column: "NomineeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblResult_CategoryId",
                table: "tblResult",
                column: "CategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblResult_NomineeId",
                table: "tblResult",
                column: "NomineeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblResult_SubscriptionId",
                table: "tblResult",
                column: "SubscriptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblRole_MovieId",
                table: "tblRole",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSubscription_Competition_Id",
                table: "tblSubscription",
                column: "Competition_Id");

            migrationBuilder.CreateIndex(
                name: "IX_tblSubscriptionNotifications_NotificationId",
                table: "tblSubscriptionNotifications",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSubscriptionNotifications_SubscriptionId",
                table: "tblSubscriptionNotifications",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblVotes_CategoryId",
                table: "tblVotes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblVotes_NomineeId",
                table: "tblVotes",
                column: "NomineeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblVotes_SubscriptionId",
                table: "tblVotes",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblVotes_UserId",
                table: "tblVotes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionUser_tblSubscription_subscritionsSubscriptionId",
                table: "SubscriptionUser",
                column: "subscritionsSubscriptionId",
                principalTable: "tblSubscription",
                principalColumn: "Subscription_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblCategory_tblCompetition_CompetitionId",
                table: "tblCategory",
                column: "CompetitionId",
                principalTable: "tblCompetition",
                principalColumn: "Competition_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblCompetition_tblResult_ResultsResultId",
                table: "tblCompetition",
                column: "ResultsResultId",
                principalTable: "tblResult",
                principalColumn: "ResultID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblResult_tblSubscription_SubscriptionId",
                table: "tblResult");

            migrationBuilder.DropForeignKey(
                name: "FK_tblAdmin_tblPerson_PersonId",
                table: "tblAdmin");

            migrationBuilder.DropForeignKey(
                name: "FK_tblCategory_tblCompetition_CompetitionId",
                table: "tblCategory");

            migrationBuilder.DropTable(
                name: "ParticipantRole");

            migrationBuilder.DropTable(
                name: "SubscriptionUser");

            migrationBuilder.DropTable(
                name: "tblCategoryNominee");

            migrationBuilder.DropTable(
                name: "tblNomineeCompetition");

            migrationBuilder.DropTable(
                name: "tblSubscriptionNotifications");

            migrationBuilder.DropTable(
                name: "tblVotes");

            migrationBuilder.DropTable(
                name: "tblParticipant");

            migrationBuilder.DropTable(
                name: "tblRole");

            migrationBuilder.DropTable(
                name: "tblNotification");

            migrationBuilder.DropTable(
                name: "tblUser");

            migrationBuilder.DropTable(
                name: "tblMovie");

            migrationBuilder.DropTable(
                name: "tblSubscription");

            migrationBuilder.DropTable(
                name: "tblPerson");

            migrationBuilder.DropTable(
                name: "tblCompetition");

            migrationBuilder.DropTable(
                name: "tblAdmin");

            migrationBuilder.DropTable(
                name: "tblResult");

            migrationBuilder.DropTable(
                name: "tblCategory");

            migrationBuilder.DropTable(
                name: "tblNominee");
        }
    }
}
