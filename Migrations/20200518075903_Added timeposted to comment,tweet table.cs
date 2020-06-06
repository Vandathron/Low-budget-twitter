using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tweeter.Migrations
{
    public partial class Addedtimepostedtocommenttweettable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TimePosted",
                table: "Tweets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TimePosted",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimePosted",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "TimePosted",
                table: "Comments");
        }
    }
}
