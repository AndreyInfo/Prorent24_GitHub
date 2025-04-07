using Microsoft.EntityFrameworkCore.Migrations;

namespace Prorent24.Context.Migrations
{
    public partial class _15_09_2019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeightUnit",
                table: "dbo_vehicles",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "dbo_vehicles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LengthUnit",
                table: "dbo_vehicles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoadCapacityUnit",
                table: "dbo_vehicles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WidthUnit",
                table: "dbo_vehicles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentUnit",
                table: "dbo_equipments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeightUnit",
                table: "dbo_equipments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LengthUnit",
                table: "dbo_equipments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PowerUnit",
                table: "dbo_equipments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VolumeUnit",
                table: "dbo_equipments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WeightUnit",
                table: "dbo_equipments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WidthUnit",
                table: "dbo_equipments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "dbo_vehicle_visibility_crew_members",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CrewMemberId = table.Column<string>(nullable: true),
                    VehicleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo_vehicle_visibility_crew_members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo_vehicle_visibility_crew_members_AspNetUsers_CrewMemberId",
                        column: x => x.CrewMemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo_vehicle_visibility_crew_members_dbo_vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "dbo_vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sys_migration_database",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MigrationName = table.Column<string>(nullable: true),
                    MigrationData = table.Column<short>(nullable: false),
                    Executed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_migration_database", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dbo_vehicle_visibility_crew_members_CrewMemberId",
                table: "dbo_vehicle_visibility_crew_members",
                column: "CrewMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_dbo_vehicle_visibility_crew_members_VehicleId",
                table: "dbo_vehicle_visibility_crew_members",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dbo_vehicle_visibility_crew_members");

            migrationBuilder.DropTable(
                name: "sys_migration_database");

            migrationBuilder.DropColumn(
                name: "HeightUnit",
                table: "dbo_vehicles");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "dbo_vehicles");

            migrationBuilder.DropColumn(
                name: "LengthUnit",
                table: "dbo_vehicles");

            migrationBuilder.DropColumn(
                name: "LoadCapacityUnit",
                table: "dbo_vehicles");

            migrationBuilder.DropColumn(
                name: "WidthUnit",
                table: "dbo_vehicles");

            migrationBuilder.DropColumn(
                name: "CurrentUnit",
                table: "dbo_equipments");

            migrationBuilder.DropColumn(
                name: "HeightUnit",
                table: "dbo_equipments");

            migrationBuilder.DropColumn(
                name: "LengthUnit",
                table: "dbo_equipments");

            migrationBuilder.DropColumn(
                name: "PowerUnit",
                table: "dbo_equipments");

            migrationBuilder.DropColumn(
                name: "VolumeUnit",
                table: "dbo_equipments");

            migrationBuilder.DropColumn(
                name: "WeightUnit",
                table: "dbo_equipments");

            migrationBuilder.DropColumn(
                name: "WidthUnit",
                table: "dbo_equipments");

            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "AspNetUsers");
        }
    }
}
