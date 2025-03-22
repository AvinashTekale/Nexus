using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRelHospitalBME : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HospitalId",
                table: "BMEAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BMEAccounts_HospitalId",
                table: "BMEAccounts",
                column: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_BMEAccounts_Hospitals_HospitalId",
                table: "BMEAccounts",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BMEAccounts_Hospitals_HospitalId",
                table: "BMEAccounts");

            migrationBuilder.DropIndex(
                name: "IX_BMEAccounts_HospitalId",
                table: "BMEAccounts");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "BMEAccounts");
        }
    }
}
