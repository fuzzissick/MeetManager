using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MeetManager.Migrations
{
    public partial class Next : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Races_Events_EventId",
                table: "Races");

            migrationBuilder.DropForeignKey(
                name: "FK_Races_Meets_MeetId",
                table: "Races");

            migrationBuilder.DropForeignKey(
                name: "FK_Races_Runners_RunnerId",
                table: "Races");

            migrationBuilder.AlterColumn<int>(
                name: "RunnerId",
                table: "Races",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MeetId",
                table: "Races",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Races",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "TeamMeet",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false),
                    MeetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMeet", x => new { x.TeamId, x.MeetId });
                    table.ForeignKey(
                        name: "FK_TeamMeet_Meets_MeetId",
                        column: x => x.MeetId,
                        principalTable: "Meets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamMeet_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamMeet_MeetId",
                table: "TeamMeet",
                column: "MeetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Events_EventId",
                table: "Races",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Meets_MeetId",
                table: "Races",
                column: "MeetId",
                principalTable: "Meets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Runners_RunnerId",
                table: "Races",
                column: "RunnerId",
                principalTable: "Runners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Races_Events_EventId",
                table: "Races");

            migrationBuilder.DropForeignKey(
                name: "FK_Races_Meets_MeetId",
                table: "Races");

            migrationBuilder.DropForeignKey(
                name: "FK_Races_Runners_RunnerId",
                table: "Races");

            migrationBuilder.DropTable(
                name: "TeamMeet");

            migrationBuilder.AlterColumn<int>(
                name: "RunnerId",
                table: "Races",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MeetId",
                table: "Races",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Races",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Events_EventId",
                table: "Races",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Meets_MeetId",
                table: "Races",
                column: "MeetId",
                principalTable: "Meets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Runners_RunnerId",
                table: "Races",
                column: "RunnerId",
                principalTable: "Runners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
