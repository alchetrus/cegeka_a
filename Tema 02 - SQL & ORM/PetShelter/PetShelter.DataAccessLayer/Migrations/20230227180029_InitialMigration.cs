using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShelter.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fundraiser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonationTarget = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fundraiser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FundraiserDonation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonorId = table.Column<int>(type: "int", nullable: false),
                    FundraiserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundraiserDonation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FundraiserDonation_Fundraiser_FundraiserId",
                        column: x => x.FundraiserId,
                        principalTable: "Fundraiser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundraiserDonation_Persons_DonorId",
                        column: x => x.DonorId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FundraiserDonation_DonorId",
                table: "FundraiserDonation",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_FundraiserDonation_FundraiserId",
                table: "FundraiserDonation",
                column: "FundraiserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FundraiserDonation");

            migrationBuilder.DropTable(
                name: "Fundraiser");
        }
    }
}
