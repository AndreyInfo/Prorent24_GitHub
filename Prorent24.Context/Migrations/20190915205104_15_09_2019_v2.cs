using Microsoft.EntityFrameworkCore.Migrations;

namespace Prorent24.Context.Migrations
{
    public partial class _15_09_2019_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MigrationData",
                table: "sys_migration_database",
                nullable: true,
                oldClrType: typeof(short));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "MigrationData",
                table: "sys_migration_database",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
