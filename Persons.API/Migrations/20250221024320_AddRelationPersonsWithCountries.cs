using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persons.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationPersonsWithCountries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "country_id",
                table: "Persons",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_country_id",
                table: "Persons",
                column: "country_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_countries_country_id",
                table: "Persons",
                column: "country_id",
                principalTable: "countries",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_countries_country_id",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_country_id",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "country_id",
                table: "Persons");
        }
    }
}
