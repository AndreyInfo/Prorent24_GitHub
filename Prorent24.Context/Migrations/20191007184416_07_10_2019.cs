using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prorent24.Context.Migrations
{
    public partial class _07_10_2019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "dbo_contacts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "dbo_contact_visibility_crew_members",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CrewMemberId = table.Column<string>(nullable: true),
                    ContactId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo_contact_visibility_crew_members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo_contact_visibility_crew_members_dbo_contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "dbo_contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo_contact_visibility_crew_members_AspNetUsers_CrewMemberId",
                        column: x => x.CrewMemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "dbo_notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreationDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    CreationUserId = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Theme = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    TaskId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true),
                    RepairId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo_notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo_notifications_AspNetUsers_CreationUserId",
                        column: x => x.CreationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo_notifications_dbo_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "dbo_projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo_notifications_dbo_repairs_RepairId",
                        column: x => x.RepairId,
                        principalTable: "dbo_repairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo_notifications_dbo_tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "dbo_tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo_notifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dbo_contact_visibility_crew_members_ContactId",
                table: "dbo_contact_visibility_crew_members",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_dbo_contact_visibility_crew_members_CrewMemberId",
                table: "dbo_contact_visibility_crew_members",
                column: "CrewMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_dbo_notifications_CreationUserId",
                table: "dbo_notifications",
                column: "CreationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_dbo_notifications_ProjectId",
                table: "dbo_notifications",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_dbo_notifications_RepairId",
                table: "dbo_notifications",
                column: "RepairId");

            migrationBuilder.CreateIndex(
                name: "IX_dbo_notifications_TaskId",
                table: "dbo_notifications",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_dbo_notifications_UserId",
                table: "dbo_notifications",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dbo_contact_visibility_crew_members");

            migrationBuilder.DropTable(
                name: "dbo_notifications");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "dbo_contacts");
        }
    }
}
