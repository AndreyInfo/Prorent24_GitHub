using Microsoft.EntityFrameworkCore.Migrations;

namespace Prorent24.Context.Migrations
{
    public partial class _16_09_2016 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "dbo_projects",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "dbo_projects",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "dbo_project_visibility_crew_members",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CrewMemberId = table.Column<string>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo_project_visibility_crew_members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo_project_visibility_crew_members_AspNetUsers_CrewMemberId",
                        column: x => x.CrewMemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo_project_visibility_crew_members_dbo_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "dbo_projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dbo_project_visibility_crew_members_CrewMemberId",
                table: "dbo_project_visibility_crew_members",
                column: "CrewMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_dbo_project_visibility_crew_members_ProjectId",
                table: "dbo_project_visibility_crew_members",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dbo_project_visibility_crew_members");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "dbo_projects");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "dbo_projects");
        }
    }
}
