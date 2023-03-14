using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShelter.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class FundraiserTableChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Fundraisers_FundraiserId",
                table: "Donations");

            migrationBuilder.AddColumn<decimal>(
                name: "CollectedAmount",
                table: "Fundraisers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Fundraisers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Fundraisers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Fundraisers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 13, 23, 48, 29, 290, DateTimeKind.Local).AddTicks(3444));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Fundraisers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AlterColumn<int>(
                name: "FundraiserId",
                table: "Donations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Fundraisers_OwnerId",
                table: "Fundraisers",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Fundraisers_FundraiserId",
                table: "Donations",
                column: "FundraiserId",
                principalTable: "Fundraisers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fundraisers_Persons_OwnerId",
                table: "Fundraisers",
                column: "OwnerId",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Fundraisers_FundraiserId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Fundraisers_Persons_OwnerId",
                table: "Fundraisers");

            migrationBuilder.DropIndex(
                name: "IX_Fundraisers_OwnerId",
                table: "Fundraisers");

            migrationBuilder.DropColumn(
                name: "CollectedAmount",
                table: "Fundraisers");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Fundraisers");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Fundraisers");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Fundraisers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Fundraisers");

            migrationBuilder.AlterColumn<int>(
                name: "FundraiserId",
                table: "Donations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Fundraisers_FundraiserId",
                table: "Donations",
                column: "FundraiserId",
                principalTable: "Fundraisers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
