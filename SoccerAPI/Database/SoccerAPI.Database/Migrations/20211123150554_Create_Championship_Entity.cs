using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoccerAPI.Database.Migrations
{
    public partial class Create_Championship_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Championships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Championships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamChampionshipMapping",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChampionshipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamChampionshipMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamChampionshipMapping_Championships_ChampionshipId",
                        column: x => x.ChampionshipId,
                        principalTable: "Championships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamChampionshipMapping_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamChampionshipMapping_ChampionshipId",
                table: "TeamChampionshipMapping",
                column: "ChampionshipId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamChampionshipMapping_TeamId",
                table: "TeamChampionshipMapping",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamChampionshipMapping");

            migrationBuilder.DropTable(
                name: "Championships");
        }
    }
}
