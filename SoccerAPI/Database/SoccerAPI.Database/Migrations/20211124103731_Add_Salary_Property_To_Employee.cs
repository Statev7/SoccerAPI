using Microsoft.EntityFrameworkCore.Migrations;

namespace SoccerAPI.Database.Migrations
{
    public partial class Add_Salary_Property_To_Employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Footballers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Coaches",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Footballers");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Coaches");
        }
    }
}
