using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartDetail");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "InstallationBases");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "PurchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "PurchaseOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "PurchaseOrders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "PurchaseOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "InstallationBases",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Importance",
                table: "InstallationBases",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "InstallationBases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "InstallationBases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "InstallationBases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "InstallationBases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "InstallationBases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "InstallationBases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ContractOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "ContractOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "ContractOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "ContractOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "ContractOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "ContractOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_InstallationBases_DepartmentId",
                table: "InstallationBases",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallationBases_EquipmentId",
                table: "InstallationBases",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractOrders_DepartmentId",
                table: "ContractOrders",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractOrders_EquipmentId",
                table: "ContractOrders",
                column: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractOrders_Departments_DepartmentId",
                table: "ContractOrders",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractOrders_Equipments_EquipmentId",
                table: "ContractOrders",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstallationBases_Departments_DepartmentId",
                table: "InstallationBases",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstallationBases_Equipments_EquipmentId",
                table: "InstallationBases",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractOrders_Departments_DepartmentId",
                table: "ContractOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractOrders_Equipments_EquipmentId",
                table: "ContractOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_InstallationBases_Departments_DepartmentId",
                table: "InstallationBases");

            migrationBuilder.DropForeignKey(
                name: "FK_InstallationBases_Equipments_EquipmentId",
                table: "InstallationBases");

            migrationBuilder.DropIndex(
                name: "IX_InstallationBases_DepartmentId",
                table: "InstallationBases");

            migrationBuilder.DropIndex(
                name: "IX_InstallationBases_EquipmentId",
                table: "InstallationBases");

            migrationBuilder.DropIndex(
                name: "IX_ContractOrders_DepartmentId",
                table: "ContractOrders");

            migrationBuilder.DropIndex(
                name: "IX_ContractOrders_EquipmentId",
                table: "ContractOrders");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "InstallationBases");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "InstallationBases");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "InstallationBases");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "InstallationBases");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "InstallationBases");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "InstallationBases");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ContractOrders");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "ContractOrders");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "ContractOrders");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "ContractOrders");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "ContractOrders");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "ContractOrders");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "InstallationBases",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Importance",
                table: "InstallationBases",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "InstallationBases",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PartDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BreakDownCloseEntityId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartDetail_BreakDownClose_BreakDownCloseEntityId",
                        column: x => x.BreakDownCloseEntityId,
                        principalTable: "BreakDownClose",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartDetail_BreakDownCloseEntityId",
                table: "PartDetail",
                column: "BreakDownCloseEntityId");
        }
    }
}
