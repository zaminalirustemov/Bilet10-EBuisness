using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_buisness.Migrations
{
    public partial class deletecolumnadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SpecialTeams",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Positions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SpecialTeams");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Positions");
        }
    }
}
