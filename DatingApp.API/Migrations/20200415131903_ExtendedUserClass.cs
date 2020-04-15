using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.API.Migrations
{
    public partial class ExtendedUserClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdDate",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dateOfBirth",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "interests",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "introduction",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "lastActive",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "lookingFor",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nickName",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    url = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    dateAdded = table.Column<DateTime>(nullable: false),
                    isMain = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserId",
                table: "Photos",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropColumn(
                name: "city",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "country",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "createdDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "dateOfBirth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "interests",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "introduction",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "lastActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "lookingFor",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "nickName",
                table: "Users");
        }
    }
}
