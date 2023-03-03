using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShelter.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class FundraiserRepositoryFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Fundraiser_FundraiserId",
                table: "Donations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fundraiser",
                table: "Fundraiser");

            migrationBuilder.RenameTable(
                name: "Fundraiser",
                newName: "Fundraisers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fundraisers",
                table: "Fundraisers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Fundraisers_FundraiserId",
                table: "Donations",
                column: "FundraiserId",
                principalTable: "Fundraisers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Fundraisers_FundraiserId",
                table: "Donations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fundraisers",
                table: "Fundraisers");

            migrationBuilder.RenameTable(
                name: "Fundraisers",
                newName: "Fundraiser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fundraiser",
                table: "Fundraiser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Fundraiser_FundraiserId",
                table: "Donations",
                column: "FundraiserId",
                principalTable: "Fundraiser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
