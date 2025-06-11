using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persons.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeGuidsToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_family_group_Persons_person_id",
                table: "family_group");

            migrationBuilder.AlterColumn<string>(
                name: "person_id",
                table: "family_group",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_family_group_Persons_person_id",
                table: "family_group",
                column: "person_id",
                principalTable: "Persons",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_family_group_Persons_person_id",
                table: "family_group");

            migrationBuilder.AlterColumn<Guid>(
                name: "person_id",
                table: "family_group",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_family_group_Persons_person_id",
                table: "family_group",
                column: "person_id",
                principalTable: "Persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
