using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persons.API.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldToUsersAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "avatar_url",
                table: "sec_users",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "birth_date",
                table: "sec_users",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                table: "sec_users",
                type: "TEXT",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                table: "sec_users",
                type: "TEXT",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "sec_roles",
                type: "TEXT",
                maxLength: 256,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "avatar_url",
                table: "sec_users");

            migrationBuilder.DropColumn(
                name: "birth_date",
                table: "sec_users");

            migrationBuilder.DropColumn(
                name: "first_name",
                table: "sec_users");

            migrationBuilder.DropColumn(
                name: "last_name",
                table: "sec_users");

            migrationBuilder.DropColumn(
                name: "description",
                table: "sec_roles");
        }
    }
}
