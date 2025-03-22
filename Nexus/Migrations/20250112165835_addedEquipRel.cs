using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Migrations
{
    /// <inheritdoc />
    public partial class addedEquipRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the primary key on the 'Id' column
            migrationBuilder.DropPrimaryKey(
                name: "PK_Equipments",
                table: "Equipments");

            // Create a new column with IDENTITY
            migrationBuilder.AddColumn<int>(
                name: "NewId",
                table: "Equipments",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            // Update the 'SerialNumber' column type
            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false);

            // Add the new column as the primary key
            migrationBuilder.AddPrimaryKey(
                name: "PK_Equipments",
                table: "Equipments",
                column: "NewId");

            // Drop the old 'Id' column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Equipments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert to the previous state
            migrationBuilder.DropPrimaryKey(
                name: "PK_Equipments",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "NewId",
                table: "Equipments");

            migrationBuilder.AlterColumn<Guid>(
                name: "SerialNumber",
                table: "Equipments",
                type: "uniqueidentifier",
                nullable: false);

            // Recreate the 'Id' column without IDENTITY
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Equipments",
                type: "int",
                nullable: false);

            // Add the old primary key
            migrationBuilder.AddPrimaryKey(
                name: "PK_Equipments",
                table: "Equipments",
                column: "SerialNumber");
        }
    }
}
